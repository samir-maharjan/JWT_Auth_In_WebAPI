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


    public class ActiveUserVM
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Occupation { get; set; }
        public string Post { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        //will work on the role later, there may be many role but for now let it be string
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

    }
}
