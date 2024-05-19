using JWT_token_auth_Demo.Data;
using JWT_token_auth_Demo.Models;
using JWT_token_auth_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    agent.agent01description=agentVM.Description;
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
                    agent.agent01status = agentVM.Status;
                    agent.agent01deleted = agentVM.Deleted;
                    agent.agent01created_name = "Admin";
                    agent.agent01updated_name = "Admin";
                    agent.agent01created_date = DateTime.Now;
                    agent.agent01updated_date = DateTime.Now;
                    _dbcontext.agent01profile.Add(agent);

                    if (agentVM.AgentFile != null)
                    {
                        foreach (var imgFile in agentVM.AgentFile.ImgFile)
                        {
                            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                            string fileExtension = Path.GetExtension(imgFile!.FileName);

                            if (Array.IndexOf(allowedExtensions, fileExtension.ToLower()) == -1)
                            {
                                return BadRequest("Invalid file! Only JPG, JPEG, and PNG files are allowed.");
                            }

                            string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(imgFile!.FileName)}";
                            string yearMonthFolder = DateTime.Now.ToString("yyyy/MM");
                            string uploadsFolder = Path.Combine("AgentProfileImages", yearMonthFolder);
                            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            if (!Directory.Exists(uploadsFolder))
                            {
                                Directory.CreateDirectory(uploadsFolder);
                            }

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await imgFile!.CopyToAsync(stream);
                            }

                            // Store file information
                            var uploadedImage = ($"~/AgentProfileImages/{yearMonthFolder}/{uniqueFileName}");

                            agent02profile_img _img = new agent02profile_img();
                            _img.agent02uin = Guid.NewGuid().ToString();
                            _img.agent02agent01uin = agent.agent01uin;
                            _img.agent02img_path = uploadedImage;
                            _img.agent02img_name = imgFile!.FileName;
                            _img.agent02status = true;
                            _img.agent02deleted = false;
                            _img.agent02created_date = DateTime.Now;
                            _img.agent02updated_date = DateTime.Now;
                            _img.agent02created_name = "admin";
                            _img.agent02updated_name = "admin";

                            _dbcontext.agent02profile_img.Add(_img);
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
                        Deleted = item.agent01deleted
                    };
                    List<agent02profile_img> images = _dbcontext.agent02profile_img.Where(x => x.agent02agent01uin == item.agent01uin).ToList();

                    if (images.Count != 0)
                    {
                        foreach (var image in images)
                        {
                            AgentImgFiles img = new AgentImgFiles()
                            {
                                Name = image.agent02img_name,
                                FilePath = image.agent02img_path,
                                UploadedDate = image.agent02updated_date
                            };
                            res1.Images.Add(img);
                        }
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
                    Deleted = agent.agent01deleted
                };

                List<agent02profile_img> images = _dbcontext.agent02profile_img.Where(x => x.agent02agent01uin == agent.agent01uin).ToList();

                if (images.Count != 0)
                {
                    foreach (var image in images)
                    {
                        AgentImgFiles img = new AgentImgFiles()
                        {
                            Name = image.agent02img_name,
                            FilePath = image.agent02img_path,
                            UploadedDate = image.agent02updated_date
                        };
                        res1.Images.Add(img);
                    }
                }
                return res1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }


        [HttpPost("UpdateProductDes")]
        public async Task<ActionResult<AfterSavedResponseVM>> UpdateProductDes(AgentResponseVM res)
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
    }
}
