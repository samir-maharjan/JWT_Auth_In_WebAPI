﻿using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Models.Authentication
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
