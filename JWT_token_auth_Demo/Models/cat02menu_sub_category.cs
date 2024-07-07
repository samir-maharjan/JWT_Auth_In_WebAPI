using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Models
{
    public class cat02menu_sub_category
    {
        [Key]
        public string cat02uin { get; set; }
        public string cat02cat01uin { get; set; }
        public string cat02sub_category_title { get; set; }
        public string cat02thumbnail_img_path { get; set; }
        public string cat02sub_category_code { get; set; }
        public bool cat02status { get; set; }
        public bool cat02deleted { get; set; }
        public string cat02created_name { get; set; }
        public string cat02updated_name { get; set; }
        public DateTime cat02created_date { get; set; }
        public DateTime cat02updated_date { get; set; }
        public cat01menu_category cat01menu_category { get; set; }
        
    }
}
