using JWT_token_auth_Demo.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Viewmodel
{
    public class UserVM
    {

        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
       
      //  public EnumApplicationUserType Role { get; set; }

        [NotMapped]
        public string Password { get; set; }

    }
}
