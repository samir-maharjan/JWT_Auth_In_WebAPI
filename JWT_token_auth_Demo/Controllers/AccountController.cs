using JWT_token_auth_Demo.Data;
using JWT_token_auth_Demo.Models;
using JWT_token_auth_Demo.Viewmodel;
using JWT_token_auth_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public AccountController(UserManager<ApplicationUser> userManager,AppDbContext appDbContext)
        {
            _userManager = userManager;
            _dbcontext = appDbContext;
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
                        usr01profile_img_path = "",
                        usr01reg_role = RegVM.RegRoles,
                        usr01status = true,
                        usr01uin = user.Id
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
