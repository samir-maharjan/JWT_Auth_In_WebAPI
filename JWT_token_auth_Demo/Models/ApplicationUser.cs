using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace JWT_token_auth_Demo.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(255)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        public string LastName { get; set; }


/*        Password
ReType Password
Occupation
Post
Address
Contact Number
Email
Role
Image Upload*/
        //public EnumApplicationUserType UserType { get; set; }

    }
}
