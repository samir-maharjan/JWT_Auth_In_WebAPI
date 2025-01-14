using JWT_token_auth_Demo.Data;
using JWT_token_auth_Demo.Models;
using JWT_token_auth_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Text.RegularExpressions;
using static JWT_token_auth_Demo.Enum;

namespace JWT_token_auth_Demo.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ApplicationFormController : ControllerBase
    {

        private readonly AppDbContext _dbcontext;
        public ApplicationFormController(AppDbContext appDbContext)
        {
            _dbcontext = appDbContext;
        }

        [HttpPost("FormSubmit")]
        public async Task<ActionResult<AfterSavedResponseVM>> FormSubmit([FromForm] ApplicationFormVM applicationFormVM)
        {
            try
            {
                if (applicationFormVM != null)
                {
                    form01application appForm = new form01application();
                    appForm.form01uin = Guid.NewGuid().ToString();
                    appForm.form01first_name = applicationFormVM.FirstName ?? string.Empty;
                    appForm.form01middle_name = applicationFormVM.MiddleName;
                    appForm.form01last_name = applicationFormVM.LastName ?? string.Empty;
                    appForm.form01address = applicationFormVM.Address ?? string.Empty;
                    appForm.form01emali_address = applicationFormVM.EmailAddress ?? string.Empty;
                    appForm.form01contact_num = applicationFormVM.ContactNumber ?? string.Empty;
                    appForm.form01status = true;
                    appForm.form01deleted = false;
                    appForm.form01created_name = "Admin";
                    appForm.form01updated_name = "Admin";
                    appForm.form01created_date = DateTime.Now;
                    appForm.form01updated_date = DateTime.Now;
                    _dbcontext.form01application.Add(appForm);

                    if (applicationFormVM.ApplicationFormFile != null)
                    {
                        foreach (var imgFile in applicationFormVM.ApplicationFormFile.ImgFile)
                        {

                            string uploadedPath = await UploadFile("ApplicationFormFiles", imgFile);

                            form02application_files _img = new form02application_files();
                            _img.form02uin = Guid.NewGuid().ToString();
                            _img.form02form01uin = appForm.form01uin;
                            _img.form02img_path = uploadedPath;
                            _img.form02img_name = imgFile!.FileName;
                            _img.form02status = true;
                            _img.form02deleted = false;
                            _img.form02created_date = DateTime.Now;
                            _img.form02updated_date = DateTime.Now;
                            _img.form02created_name = "admin";
                            _img.form02updated_name = "admin";

                            _dbcontext.form02application_files.Add(_img);
                        }
                    }
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
        [HttpPost("FilterApplicationFormList")]

        public async Task<IEnumerable<AppFormResponseVM>> FilterApplicationFormList([FromForm] ApplicationFormSearchVM filter)
        {
            try
            {
                // Start the query
                var query = _dbcontext.form01application
                                      .Where(x => !x.form01deleted)
                                      .AsQueryable();

                // Apply filters conditionally
                if (filter.Status.HasValue)
                {
                    query = query.Where(x => x.form01status == filter.Status.Value);
                }

                if (!string.IsNullOrEmpty(filter.EmailAddress))
                {
                    query = query.Where(x => x.form01emali_address.Contains(filter.EmailAddress));
                }

                if (!string.IsNullOrEmpty(filter.Address))
                {
                    query = query.Where(x => x.form01address.Contains(filter.Address));
                }

                if (!string.IsNullOrEmpty(filter.ContactNumber))
                {
                    query = query.Where(x => x.form01contact_num.Contains(filter.ContactNumber));
                }

                if (filter.UpdatedDate.HasValue)
                {
                    query = query.Where(x => x.form01created_date <= filter.UpdatedDate.Value);
                }

                // Execute the query
                var res = await query
                    .OrderByDescending(x => x.form01created_date)
                    .Take(100)
                    .ToListAsync();

                // Map results to response view model
                IList<AppFormResponseVM> resList = new List<AppFormResponseVM>();
                foreach (var item in res)
                {
                    var res1 = new AppFormResponseVM
                    {
                        ID = item.form01uin,
                        FirstName = item.form01first_name,
                        MiddleName = item.form01middle_name,
                        LastName = item.form01last_name,
                        EmailAddress = item.form01emali_address,
                        ContactNumber = item.form01contact_num,
                        Address = item.form01address,
                        Description = item.form01description ?? string.Empty,
                        Status = item.form01status,
                        Deleted = item.form01deleted,
                        CreatedDate = item.form01created_date,
                        UpdatedDate = item.form01updated_date,
                        Images = new List<AppFormFiles>()
                    };

                    // Get related images
                    var images = _dbcontext.form02application_files
                        .Where(x => x.form02form01uin == item.form01uin &&
                                    !x.form02deleted &&
                                    x.form02status)
                        .ToList();

                    foreach (var image in images)
                    {
                        res1.Images.Add(new AppFormFiles
                        {
                            ImageId = image.form02uin,
                            Name = image.form02img_name,
                            FilePath = image.form02img_path,
                            UploadedDate = image.form02updated_date
                        });
                    }

                    resList.Add(res1);
                }

                return resList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching application forms", ex);
            }
        }

        [HttpGet("ApplicationFormList")]
        public async Task<IEnumerable<AppFormResponseVM>> ApplicationFormList()
        {
            try
            {
                List<form01application> res = await _dbcontext.form01application
                                                //.Where(x => !x.form01deleted)
                                                .OrderByDescending(x => x.form01created_date)
                                                .Take(100)
                                                .ToListAsync();
                IList<AppFormResponseVM> resList = new List<AppFormResponseVM>();
                foreach (var item in res)
                {
                    AppFormResponseVM res1 = new AppFormResponseVM()
                    {
                        ID = item.form01uin,
                        FirstName = item.form01first_name,
                        MiddleName = item.form01middle_name,
                        LastName = item.form01last_name,
                        EmailAddress = item.form01emali_address,
                        ContactNumber = item.form01contact_num,
                        Address = item.form01address,
                        Description = item.form01description ?? string.Empty,
                        Status = item.form01status,
                        Deleted = item.form01deleted,
                        CreatedDate = item.form01created_date,
                        UpdatedDate = item.form01updated_date
                    };
                    List<form02application_files> images = _dbcontext.form02application_files.Where(x => x.form02form01uin == item.form01uin && !x.form02deleted && x.form02status).ToList();

                    if (images.Any()) // Use .Any() instead of checking Count for better readability
                    {
                        res1.Images.AddRange(images.Select(image => new AppFormFiles
                        {
                            ImageId = image.form02uin,
                            Name = image.form02img_name,
                            FilePath = image.form02img_path,
                            UploadedDate = image.form02updated_date
                        }));
                    }
                    resList.Add(res1);
                }

                return resList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }
        [HttpGet("UpdateApplicationForm")]
        public async Task<AppFormResponseVM> UpdateApplicationForm(string id)
        {
            try
            {
                form01application appForm = _dbcontext.form01application.Where(x => x.form01uin == id).FirstOrDefault();
                if (appForm == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
              
                    AppFormResponseVM res1 = new AppFormResponseVM()
                    {
                        ID = appForm.form01uin,
                        FirstName = appForm.form01first_name,
                        MiddleName = appForm.form01middle_name,
                        LastName = appForm.form01last_name,
                        EmailAddress = appForm.form01emali_address,
                        ContactNumber = appForm.form01contact_num,
                        Address = appForm.form01address,
                        Description = appForm.form01description ?? string.Empty,
                        Status = appForm.form01status,
                        Deleted = appForm.form01deleted,
                        CreatedDate = appForm.form01created_date,
                        UpdatedDate = appForm.form01updated_date
                    };

                List<form02application_files> images = _dbcontext.form02application_files.Where(x => x.form02form01uin == appForm.form01uin && !x.form02deleted && x.form02status).ToList();


                if (images.Any()) // Use .Any() instead of checking Count for better readability
                {
                    res1.Images.AddRange(images.Select(image => new AppFormFiles
                    {
                        ImageId = image.form02uin,
                        Name = image.form02img_name,
                        FilePath = image.form02img_path,
                        UploadedDate = image.form02updated_date
                    }));
                }
                return res1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }
        [HttpPost("UpdateApplicationForm")]
        public async Task<ActionResult<AfterSavedResponseVM>> UpdateApplicationForm([FromForm] ApplicationFormVM res)
        {
            try
            {
                form01application formDetails = _dbcontext.form01application.Where(x => x.form01uin == res.ID).FirstOrDefault();
                if (formDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                formDetails.form01first_name = res.FirstName;
                formDetails.form01middle_name = res.MiddleName;
                formDetails.form01last_name = res.LastName;
                formDetails.form01address = res.Address;
                formDetails.form01emali_address = res.EmailAddress;
                formDetails.form01contact_num = res.ContactNumber;
                formDetails.form01description = res.Description;
                formDetails.form01status = res.Status;
                formDetails.form01deleted = res.Deleted;
                formDetails.form01updated_date = DateTime.Now;
                formDetails.form01updated_name = "admin";

                _dbcontext.form01application.Update(formDetails);

                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new AfterSavedResponseVM { status = false, error_msg = ex.ToString() };
                throw new Exception("Error:", ex);
            }
            return new AfterSavedResponseVM { status = true };
        }
        private async Task<string> UploadFile(string folderName, IFormFile file)
        {
            try
            {
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };
                string fileExtension = Path.GetExtension(file!.FileName);

                if (Array.IndexOf(allowedExtensions, fileExtension.ToLower()) == -1)
                {
                    throw new InvalidOperationException("Invalid file! Only JPG, JPEG, PNG, and PDF files are allowed.");
                }

                string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                string yearMonthFolder = DateTime.Now.ToString("yyyy/MM");
                string uploadsFolder = Path.Combine(folderName, yearMonthFolder);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Store file information
                var uploadedImage = Path.Combine("~", folderName, yearMonthFolder, uniqueFileName).Replace("\\", "/");
                return uploadedImage;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while uploading the file.", ex);
            }
        }

   
    }
}
