﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JWT_token_auth_Demo.Data;
using JWT_token_auth_Demo.Models;
using JWT_token_auth_Demo.Models.Authentication;
using JWT_token_auth_Demo.Viewmodel;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
//using QA_Test_Log.Email;
using Microsoft.AspNetCore.Cors;

namespace JWT_token_auth_Demo.Controllers
{

    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext dbContext;

        public LoginController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            dbContext = context;
        }
        

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)

        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userDetails = dbContext.usr01users.Where(x => x.usr01uin == user.Id).FirstOrDefault();
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                var usersInfo = new ActiveUserVM
                {
                    UserName = user.UserName,
                    ID = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Occupation = userDetails.usr01occupation,
                    Post = userDetails.usr01post,
                    ContactNumber=userDetails.usr01contact_number,
                    ProfileImagePath= userDetails.usr01profile_img_path,
                    Role = userDetails.usr01reg_role,
                    Address = userDetails.usr01address,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };


                return Ok(usersInfo);
            }
            return Unauthorized();
        }
        [HttpGet]
        [Route("SeedDefaultData")]

 

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordData)
        {
            try
            {
                // Find the user by their email (assuming email is unique)
                var user = await _userManager.FindByEmailAsync(changePasswordData.Email);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Compare the password from the database with the current password provided in the request body
                var passwordCheckResult = await _userManager.CheckPasswordAsync(user, changePasswordData.CurrentPassword);

                if (!passwordCheckResult)
                {
                    return BadRequest("Current password is incorrect.");
                }

                // Validate that the new password and confirmation password match
                if (changePasswordData.NewPassword != changePasswordData.ConfirmPassword)
                {
                    return BadRequest("The new password and confirmation password do not match.");
                }

                // Use the ChangePasswordAsync method to change the user's password.
                var result = await _userManager.ChangePasswordAsync(user, changePasswordData.CurrentPassword, changePasswordData.NewPassword);

                if (result.Succeeded)
                {


                    return Ok("Password Changed Successfully");
                }
                else
                {
                    // Handle password change failure scenarios, if needed
                    string allErrMsg = string.Join(",", result.Errors.Select(x => x.Description).ToArray());
                    return BadRequest("Password change failed. " + allErrMsg);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, if any
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [HttpPost("Forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVm model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return NotFound(new { message = "Email not found." });
                }

                var otpCode = GenerateRandomOTP();

                OTPVerificationLogs passwordResetRequest = new OTPVerificationLogs()
                {
                    Email = model.Email,
                    GeneratedOTPCode = otpCode,
                    CreatedAt = DateTime.UtcNow,
                    ExpirationTime = DateTime.UtcNow.AddMinutes(30), // Set an expiration time
                };

                dbContext.OTPVerificationLogs.Add(passwordResetRequest);
                await dbContext.SaveChangesAsync();

                //await SendEmail.SendEmailAsync(_configuration,model.Email, "Mail has arrived. OTP Code: " + otpCode, "");

                return Ok(new { message = "An OTP code has been sent to your email address. Please check your email and use the code to reset your password." });
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                return BadRequest(new { message = "An error occurred: " + ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "") });
            }
        }

        private string GenerateRandomOTP()
        {

            var otpCode = new Random().Next(100000, 999999).ToString();
            return otpCode;
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordVm model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return NotFound(new { message = "Email not found." });
                }

                // Find the latest OTP verification log for the given email
/*                var otpVerification = dbContext.OTPVerificationLogs
                    .Where(log => log.Email == model.Email && log.ExpirationTime >= DateTime.UtcNow)
                    .OrderByDescending(log => log.CreatedAt)
                    .FirstOrDefault();

                if (otpVerification == null || otpVerification.GeneratedOTPCode != model.GeneratedOTPCode)
                {
                    return BadRequest(new { message = "Invalid OTP code." });
                }

                if (model.NewPassword != model.ConfirmPassword)
                {
                    return BadRequest(new { message = "New password and confirm password do not match." });
                }
*/
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);


                // At this point, the OTP code is valid, and the new password matches the confirm password
                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

                if (resetPasswordResult.Succeeded)
                {
                    // Delete the OTPVerificationLog entry since it's no longer needed
                  //  dbContext.OTPVerificationLogs.Remove(otpVerification);
                    await dbContext.SaveChangesAsync();

                    return Ok(new { message = "Password has been reset successfully." });
                }
                else
                {
                    // Handle password reset errors
                    return BadRequest(new { message = "Failed to reset the password. Please try again." });
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                return BadRequest(new { message = "An error occurred: " + ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "") });
            }
        }

    }



}

