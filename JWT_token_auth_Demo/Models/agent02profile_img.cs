
using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Models
{
    public class agent02profile_img
    {
        [Key]
        public string agent02uin { get; set; }
        public string agent02agent01uin { get; set; }
        public string agent02img_path { get; set; }
        public string agent02img_name { get; set; }
        public bool agent02status { get; set; }
        public bool agent02deleted { get; set; }
        public string agent02created_name { get; set; }
        public string agent02updated_name { get; set; }
        public DateTime agent02created_date { get; set; }
        public DateTime agent02updated_date { get; set; }
        public agent01profile agent01profile { get; set; }

    }
}
