using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Viewmodel
{
    public class ForgotPasswordVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
    }
}
