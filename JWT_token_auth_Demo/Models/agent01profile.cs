using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Models
{
    public class agent01profile
    {
        [Key]
        public string agent01uin { get; set; }
        public string agent01name { get; set; }
        public string agent01code { get; set; }
        public string agent01designation { get; set; }
        public string agent01address { get; set; }
        public string agent01experience { get; set; }
        public string agent01email { get; set; }
        public string agent01skill { get; set; }
        public string agent01contact { get; set; }
        public string agent01description { get; set; }
        public string agent01fb_link { get; set; }
        public string agent01website_link { get; set; }
        public string agent01linked_in_profile { get; set; }
        public string agent01profile_img_path { get; set; }
        public bool agent01status { get; set; }
        public bool agent01deleted { get; set; }
        public string agent01created_name { get; set; }
        public string agent01updated_name { get; set; }
        public DateTime agent01created_date { get; set; }
        public DateTime agent01updated_date { get; set; }

    }
}
