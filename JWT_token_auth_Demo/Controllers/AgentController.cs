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

namespace JWT_token_auth_Demo.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class AgentController : ControllerBase
    {

        private readonly AppDbContext _dbcontext;
        public AgentController(AppDbContext appDbContext)
        {
            _dbcontext = appDbContext;
        }

        [HttpPost("CreateAgent")]
        public async Task<ActionResult<AfterSavedResponseVM>> CreateAgent([FromForm] AgentVM agentVM)
        {
            try
            {
                if (agentVM != null)
                {
                    agent01profile agent = new agent01profile();
                    agent.agent01uin = Guid.NewGuid().ToString();
                    agent.agent01description = agentVM.Description;
                    agent.agent01name = agentVM.AgentName;
                    agent.agent01code = agentVM.AgentCode;
                    agent.agent01designation = agentVM.Designation;
                    agent.agent01address = agentVM.Address;
                    agent.agent01experience = agentVM.Experience;
                    agent.agent01email = agentVM.Email;
                    agent.agent01skill = agentVM.Skill;
                    agent.agent01contact = agentVM.Contact;
                    agent.agent01fb_link = agentVM.FBLink;
                    agent.agent01website_link = agentVM.WebsiteLink;
                    agent.agent01linked_in_profile = agentVM.LinkedInLink;
                    agent.agent01status = true;
                    agent.agent01deleted = false;
                    agent.agent01created_name = "Admin";
                    agent.agent01updated_name = "Admin";
                    agent.agent01created_date = DateTime.Now;
                    agent.agent01updated_date = DateTime.Now;
                    
                    if(agentVM.ImgFile != null)
                    {

                        string uploadedImage = await UploadFile("AgentProfileImages", agentVM.ImgFile);
                        agent.agent01profile_img_path = uploadedImage;
                    }
                    _dbcontext.agent01profile.Add(agent);
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

        [HttpGet("AgentsList")]
        public async Task<IEnumerable<AgentResponseVM>> AgentsList()
        {
            try
            {
                List<agent01profile> res = await _dbcontext.agent01profile.Where(x => !x.agent01deleted && x.agent01status).ToListAsync();
                IList<AgentResponseVM> resList = new List<AgentResponseVM>();
                foreach (var item in res)
                {
                    AgentResponseVM res1 = new AgentResponseVM()
                    {
                        ID = item.agent01uin,
                        AgentCode = item.agent01code,
                        AgentName = item.agent01name,
                        Designation = item.agent01designation,
                        Address = item.agent01address,
                        Experience = item.agent01experience,
                        Skill = item.agent01skill,
                        Email = item.agent01email,
                        Description = item.agent01designation,
                        Contact = item.agent01contact,
                        FBLink = item.agent01fb_link,
                        WebsiteLink = item.agent01website_link,
                        LinkedInLink = item.agent01fb_link,
                        Status = item.agent01status,
                        Deleted = item.agent01deleted,
                        AgentImgPath = item.agent01profile_img_path
                    };
                    resList.Add(res1);
                }

                return resList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }


        }

        [HttpGet("UpdateAgents")]
        public async Task<AgentResponseVM> UpdateAgents(string id)
        {
            try
            {
                agent01profile agent = _dbcontext.agent01profile.Where(x => x.agent01uin == id).FirstOrDefault();
                if (agent == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                AgentResponseVM res1 = new AgentResponseVM()
                {
                    ID = agent.agent01uin,
                    AgentCode = agent.agent01code,
                    AgentName = agent.agent01name,
                    Designation = agent.agent01designation,
                    Address = agent.agent01address,
                    Experience = agent.agent01experience,
                    Skill = agent.agent01skill,
                    Email = agent.agent01email,
                    Description = agent.agent01designation,
                    Contact = agent.agent01contact,
                    FBLink = agent.agent01fb_link,
                    WebsiteLink = agent.agent01website_link,
                    LinkedInLink = agent.agent01fb_link,
                    Status = agent.agent01status,
                    Deleted = agent.agent01deleted,
                    AgentImgPath = agent.agent01profile_img_path ==null? "" : agent.agent01profile_img_path
                };
                return res1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }


        [HttpPost("UpdateAgents")]
        public async Task<ActionResult<AfterSavedResponseVM>> UpdateAgents([FromForm] AgentVM res)
        {
            try
            {
                agent01profile agentDetails = _dbcontext.agent01profile.Where(x => x.agent01uin == res.ID).FirstOrDefault();
                if (agentDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                agentDetails.agent01name = res.AgentName;
                agentDetails.agent01code = res.AgentCode;
                agentDetails.agent01designation = res.Designation;
                agentDetails.agent01address = res.Address;
                agentDetails.agent01experience = res.Experience;
                agentDetails.agent01email = res.Email;
                agentDetails.agent01skill = res.Skill;
                agentDetails.agent01contact = res.Contact;
                agentDetails.agent01description = res.Description;
                agentDetails.agent01fb_link = res.FBLink;
                agentDetails.agent01website_link = res.WebsiteLink;
                agentDetails.agent01linked_in_profile = res.LinkedInLink;
                agentDetails.agent01status = res.Status;
                agentDetails.agent01deleted = res.Deleted;
                agentDetails.agent01updated_date = DateTime.Now;
                agentDetails.agent01updated_name = "admin";

                if (res.ImgFile != null)
                {
                    if (agentDetails.agent01profile_img_path != null)
                    {
                        DeleteProfileImg(agentDetails.agent01profile_img_path);
                    }
                    string newFilePath = await UploadFile("AgentProfileImages", res.ImgFile);
                    agentDetails.agent01profile_img_path = newFilePath;

                }
                _dbcontext.agent01profile.Update(agentDetails);

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
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                string fileExtension = Path.GetExtension(file!.FileName);

                if (Array.IndexOf(allowedExtensions, fileExtension.ToLower()) == -1)
                {
                    throw new InvalidOperationException("Invalid file! Only JPG, JPEG, and PNG files are allowed.");
                }

                string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                string yearMonthFolder = DateTime.Now.ToString("yyyy/MM");
                string uploadsFolder = Path.Combine(folderName, yearMonthFolder);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                /*  using (var stream = new FileStream(filePath, FileMode.Create))
                  {
                      await file.CopyToAsync(stream);
                  }*/

                using (var image = Image.Load(file.OpenReadStream()))
                {
                    // Resize the image (optional)
                    /* image.Mutate(x => x.Resize(new ResizeOptions
                     {
                         Mode = ResizeMode.Max,
                         Size = new Size(800, 600) // Adjust dimensions as needed
                     }));*/

                    // Save the compressed image
                    await image.SaveAsync(filePath, new JpegEncoder
                    {
                        Quality = 50 // Adjust quality as needed
                    });
                }

                // Store file information
                var uploadedImage = Path.Combine("~", folderName, yearMonthFolder, uniqueFileName).Replace("\\", "/");
                return uploadedImage;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while uploading the file.", ex);
            }
        }

        private IActionResult DeleteProfileImg(string relativePath)
        {
            try
            {
                var absolutePath = Path.Combine(relativePath.TrimStart('~').TrimStart('/'));
                if (System.IO.File.Exists(absolutePath))
                {
                    System.IO.File.Delete(absolutePath); // delete file from the file path
                    return Ok("File deleted successfully.");
                }
                return NotFound("File not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
