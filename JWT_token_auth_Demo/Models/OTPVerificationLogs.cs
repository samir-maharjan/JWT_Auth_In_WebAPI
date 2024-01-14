using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Models
{
    public class OTPVerificationLogs
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string GeneratedOTPCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
