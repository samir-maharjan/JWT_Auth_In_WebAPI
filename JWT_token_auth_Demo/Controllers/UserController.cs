using JWT_token_auth_Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using JWT_token_auth_Demo.Viewmodel;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Cors;

namespace JWT_token_auth_Demo.Controllers
{
    //[Authorize(Roles = "SystemAdmin")]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)

        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users.Select(u => new
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                Password = u.PasswordHash, // Include the password hash if needed
                //UserType = u.UserType // Include the UserType property if it's part of your ApplicationUser
            }).ToList();

            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Project the user to include desired columns
            var userData = new
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash, // Include the password hash if needed
                //UserType = user.UserType // Include the UserType property if it's part of your ApplicationUser
            };

            return Ok(userData);
        }

        [HttpPost]
        public async Task<ActionResult<UserVM>> CreateRelatedIdentityUser(UserVM Data)
        {
            try
            {
                // Check if the email is already in use
                var existingUser = await _userManager.FindByEmailAsync(Data.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email address is already in use.");
                }

                var user = new ApplicationUser
                {

                    UserName = Data.Username,
                    FirstName = Data.FirstName,
                    LastName = Data.LastName,
                    Email = Data.Email,
                  //  UserType = Data.Role // Set UserType
                };

                var result = await _userManager.CreateAsync(user, Data.Password);

                if (result.Succeeded)
                {/*
                    if (Data.Role == EnumApplicationUserType.SystemAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, EnumApplicationUserType.SystemAdmin.ToString());
                    }
                    if (Data.Role == EnumApplicationUserType.SystemUsers)
                    {
                        await _userManager.AddToRoleAsync(user, EnumApplicationUserType.SystemUsers.ToString());
                    }*/

                    // Create a UserVM instance to represent the created user
                    var createdUserVM = new UserVM
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                       // Role = user.UserType // Assuming Role property in UserVM represents UserType
                    };

                    return Ok(new { Message = "User created successfully", User = createdUserVM });
                }
                else
                {
                    // Handle other user creation failure scenarios, if needed
                    string allErrMsg = string.Join(",", result.Errors.Select(x => x.Description).ToArray());
                    return BadRequest("User creation failed. " + allErrMsg);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserVM updatedUserData)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties based on the provided updatedUserData
            user.UserName = updatedUserData.Username;
            user.FirstName = updatedUserData.FirstName;
            user.LastName = updatedUserData.LastName;
            user.Email = updatedUserData.Email;
            //user.UserType = updatedUserData.Role;

            // Handle updating other properties here as needed

            // Check if the password needs to be updated
            if (!string.IsNullOrEmpty(updatedUserData.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, updatedUserData.Password);

                if (!result.Succeeded)
                {
                    string allErrMsg = string.Join(",", result.Errors.Select(x => x.Description).ToArray());
                    return BadRequest("Password update failed. " + allErrMsg);
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
            {
                // Project the updated user to include desired columns
                var updatedUser = new
                {
                    Id = user.Id,
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                   // UserType = user.UserType // Include the UserType property if it's part of your ApplicationUser
                };

                return Ok(updatedUser);
            }
            else
            {
                // Handle user update failure scenarios, if needed
                string allErrMsg = string.Join(",", updateResult.Errors.Select(x => x.Description).ToArray());
                return BadRequest("User update failed. " + allErrMsg);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                // Return a success message in the response body
                var successResponse = new
                {
                    Message = "User deleted successfully"
                };

                // Use Ok to return a 200 OK status code along with the success message
                return Ok(successResponse);
            }
            else
            {
                // Handle user deletion failure scenarios, if needed
                string allErrMsg = string.Join(",", result.Errors.Select(x => x.Description).ToArray());
                return BadRequest("User deletion failed. " + allErrMsg);
            }
        }

    }
}




