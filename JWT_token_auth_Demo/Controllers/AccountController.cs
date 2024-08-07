﻿using JWT_token_auth_Demo.Data;
using JWT_token_auth_Demo.Models;
using JWT_token_auth_Demo.Viewmodel;
using JWT_token_auth_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;

namespace JWT_token_auth_Demo.Controllers
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _dbcontext;
        private readonly IWebHostEnvironment environment;

        public AccountController(UserManager<ApplicationUser> userManager,AppDbContext appDbContext, IWebHostEnvironment _environment)
        {
            _userManager = userManager;
            _dbcontext = appDbContext;
            environment = _environment;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<AfterSavedResponseVM>>  Register([FromForm] RegisterViewModel RegVM)
        {

            /*if (!(_ActiveSession.IsSuperAdmin || _ActiveSession.IsOrgAdmin || _ActiveSession.IsAdmin))
            {
                return RedirectToAction("Index");
            }*/
            try
            {

                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                string fileExtension = Path.GetExtension(RegVM.ProfileImg!.FileName);

                if (Array.IndexOf(allowedExtensions, fileExtension.ToLower()) == -1)
                {
                    return BadRequest("Invalid file extension. Only JPG, JPEG, and PNG files are allowed.");
                }

                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(RegVM.ProfileImg!.FileName)}";
                string yearMonthFolder = DateTime.Now.ToString("yyyy/MM");
                string uploadsFolder = Path.Combine("ProfileImages", yearMonthFolder);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var originalImageStream = RegVM.ProfileImg!.OpenReadStream())
                {
                    using (var image = Image.Load(originalImageStream))
                    {
                        // Resize the image (optional)
                        /*image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Max,
                            Size = new Size(800, 600) // Adjust dimensions as needed
                        }));*/

                        // Save the compressed image to a memory stream
                        using (var memoryStream = new MemoryStream())
                        {
                            image.Save(memoryStream, new JpegEncoder
                            {
                                Quality = 75 // Adjust quality as needed
                            });

                            // Compare sizes
                            if (memoryStream.Length < originalImageStream.Length)
                            {
                                // Save compressed image to file
                                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                                {
                                    memoryStream.Position = 0;
                                    await memoryStream.CopyToAsync(fileStream);
                                }
                            }
                            else
                            {
                                // Save original image to file if it's smaller or the same size
                                originalImageStream.Position = 0; // Reset stream position
                                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                                {
                                    await originalImageStream.CopyToAsync(fileStream);
                                }
                            }
                        }
                    }
                }


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await RegVM.ProfileImg!.CopyToAsync(stream);
                }

                // Store file information
                var uploadedProfileImage = ($"~/ProfileImages/{yearMonthFolder}/{uniqueFileName}");

                var existingUser = await _userManager.FindByEmailAsync(RegVM.Email);
                if (existingUser != null)
                {
                    throw new Exception("Email Already Been Registered.");
                }

                var user = new ApplicationUser
                {

                    UserName = RegVM.UserName,
                    FirstName = RegVM.FirstName,
                    LastName = RegVM.LastName,
                    Email = RegVM.Email,
                    //  UserType = Data.Role // Set UserType
                };

                var result = await _userManager.CreateAsync(user, RegVM.Password);

                if (result.Succeeded)
                {
                    usr01users UserData = new usr01users()
                    {
                        usr01user_name = RegVM.UserName,
                        usr01address = RegVM.Address,
                        usr01approved = true,//for now its statically true
                        usr01contact_number = RegVM.ContactNo,
                        usr01deleted = false,
                        usr01email = RegVM.Email,
                        usr01first_name = RegVM.FirstName,
                        usr01last_name = RegVM.LastName,
                        usr01occupation = RegVM.Occupation,
                        usr01post = RegVM.Post,
                        usr01reg_role = RegVM.RegRoles,
                        usr01status = true,
                        usr01uin = user.Id,
                        // Assign uploaded file information to VideoKycInfo model properties
                        //videoKycInfo.ImageFileName = string.Join(",", bankFiles.Select(f => f.FileName));
                        usr01profile_img_path = string.Join(",", uploadedProfileImage),
                        usr01created_name = "admin",
                        usr01updated_name ="admin",
                        usr01created_date=DateTime.Now,
                        usr01updated_date = DateTime.Now
                    };

                    //todo:Have to work on the logs also
                    /*  log05account_activities log05account_activities = new log05account_activities()
                      {
                          log05created_date = _ActiveSession.SysDateEng,
                          log05emp01uin = userData.EmployeeId,
                          log05usr05userId = RegUser.Id,
                          log05bra01uin = userData.BranchId,
                          log05IpAddress = HttpContext.Request.UserHostAddress,
                          log05ComputerName = HttpContext.Request.UserHostName,
                          log05remarks = "Account Created By: " + (_ActiveSession.IsSuperAdmin ? "SuperAdmin" : _ActiveSession.EmployeeName),
                      };
                      User.log05account_activities.Add(log05account_activities);*/
                    _dbcontext.usr01users.Add(UserData);
                    _dbcontext.SaveChanges();
                }
               

            }
            catch (Exception ex)
            {
                return new AfterSavedResponseVM { status = false, error_msg = ex.ToString() };
                throw new Exception("Error:", ex);
            }
            return new AfterSavedResponseVM { status = true };

        }
    }
}
