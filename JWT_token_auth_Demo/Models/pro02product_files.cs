using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Models
{
    public class pro02product_files
    {
        [Key]
        public string pro02uin { get; set; }
        public string pro02pro01uin { get; set; }
        public string pro02img_path { get; set; }
        public string pro02img_name { get; set; }
        public bool pro02status { get; set; }
        public bool pro02deleted { get; set; }
        public string pro02created_name { get; set; }
        public string pro02updated_name { get; set; }
        public DateTime pro02created_date { get; set; }
        public DateTime pro02updated_date { get; set; }
        public pro01product pro01product { get; set; }

    }
}
