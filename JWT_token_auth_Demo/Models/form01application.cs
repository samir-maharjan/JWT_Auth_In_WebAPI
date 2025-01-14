using System.ComponentModel.DataAnnotations;
using static JWT_token_auth_Demo.Enum;

namespace JWT_token_auth_Demo.Models
{
    public class form01application
    {
        [Key]
        public string form01uin { get; set; }
        public string form01first_name { get; set; }
        public string? form01middle_name { get; set; }
        public string form01last_name { get; set; }
        public string form01address { get; set; }
        public string form01emali_address { get; set; }
        public string form01contact_num { get; set; }
        public string? form01description { get; set; }
        public bool form01status { get; set; }
        public bool form01deleted { get; set; }
        public string form01created_name { get; set; }
        public string form01updated_name { get; set; }
        public DateTime form01created_date { get; set; }
        public DateTime form01updated_date { get; set; }

        public ICollection<form02application_files> form02application_files { get; set; }


    }
}
