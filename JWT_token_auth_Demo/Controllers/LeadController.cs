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
    public class LeadController : ControllerBase
    {

        private readonly AppDbContext _dbcontext;
        public LeadController(AppDbContext appDbContext)
        {
            _dbcontext = appDbContext;
        }

        [HttpPost("CreateLead")]
        public async Task<ActionResult<AfterSavedResponseVM>> CreateLead(LeadVM leadVM)
        {
            try
            {
                if (leadVM != null)
                {
                    var categoryName = _dbcontext.cat01menu_category.Where(x => x.cat01uin == leadVM.Category).Select(x => x.cat01category_title).FirstOrDefault();
                    var agentName = _dbcontext.agent01profile.Where(x => x.agent01uin == leadVM.Agent).Select(x => x.agent01name).FirstOrDefault();
                    lead01lead_info lead = new lead01lead_info();
                    lead.lead01uin = Guid.NewGuid().ToString();
                    lead.lead0first_name = leadVM.FirstName;
                    lead.lead0middle_name = leadVM.MiddleName;
                    lead.lead0last_name = leadVM.LastName;
                    lead.lead01address = leadVM.Address;
                    lead.lead01phone_number = leadVM.Phonenumber;
                    lead.lead01email = leadVM.Email;
                    lead.lead01property = leadVM.Property;
                    lead.lead01category = categoryName == null ? "" : categoryName;
                    lead.lead01agent = agentName == null ? "" : agentName;
                    lead.lead01query_message = leadVM.QueryMessgae;
                    lead.lead01status = true;
                    lead.lead01deleted = false;
                    lead.lead01created_name = "Admin";
                    lead.lead01updated_name = "Admin";
                    lead.lead01created_date = DateTime.Now;
                    lead.lead01updated_date = DateTime.Now;

                    _dbcontext.lead01lead_info.Add(lead);
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

        [HttpGet("leadList")]
        public async Task<IEnumerable<LeadVM>> leadList()
        {
            try
            {
                List<lead01lead_info> res = await _dbcontext.lead01lead_info.Where(x => !x.lead01deleted).ToListAsync();
                IList<LeadVM> resList = new List<LeadVM>();
                foreach (var item in res)
                {
                    LeadVM res1 = new LeadVM()
                    {
                        Id = item.lead01uin,
                        FirstName = item.lead0first_name,
                        MiddleName = item.lead0middle_name,
                        LastName = item.lead0last_name,
                        Address = item.lead01address,
                        Phonenumber = item.lead01phone_number,
                        Email = item.lead01email,
                        Property = item.lead01property,
                        Category = item.lead01category,
                        Agent = item.lead01agent,
                        QueryMessgae = item.lead01query_message,
                        Status = item.lead01status,
                        Deleted = item.lead01deleted,
                        CreatedDate =item.lead01created_date,
                        UpdatedDate =item.lead01updated_date
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


        [HttpGet("UpdateLead")]
        public async Task<LeadVM> UpdateLead(string id)
        {
            try
            {
                lead01lead_info? item = _dbcontext.lead01lead_info.Where(x => x.lead01uin == id).FirstOrDefault();
                if (item == null)
                {
                    throw new Exception("Error:Data Not Found!");
                }
                LeadVM res1 = new LeadVM()
                {
                    Id = item.lead01uin,
                    FirstName = item.lead0first_name,
                    MiddleName = item.lead0middle_name,
                    LastName = item.lead0last_name,
                    Address = item.lead01address,
                    Phonenumber = item.lead01phone_number,
                    Email = item.lead01email,
                    Property = item.lead01property,
                    Category = item.lead01category,
                    Agent = item.lead01agent,
                    QueryMessgae = item.lead01query_message,
                    Status = item.lead01status,
                    Deleted = item.lead01deleted,
                    CreatedDate = item.lead01created_date,
                    UpdatedDate = item.lead01updated_date
                };
                return res1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }


        [HttpPost("UpdateLead")]
        public async Task<ActionResult<AfterSavedResponseVM>> Updatelead(LeadVM res)
        {
            try
            {
                lead01lead_info? leadDetails = _dbcontext.lead01lead_info.Where(x => x.lead01uin == res.Id).FirstOrDefault();
                if (leadDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");
                }
                leadDetails.lead01status = res.Status;
                leadDetails.lead01deleted = res.Deleted;
                leadDetails.lead01updated_date = DateTime.Now;
                leadDetails.lead01updated_name = "admin";

                _dbcontext.lead01lead_info.Update(leadDetails);
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
