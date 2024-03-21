using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Models
{
    public class cat01menu_category
    {
        [Key]
        public string cat01uin { get; set; }
        public string cat01category_title { get; set; }
        public string cat01category_code { get; set; }
        public bool cat01status { get; set; }
        public bool cat01deleted { get; set; }
        public string cat01created_name { get; set; }
        public string cat01updated_name { get; set; }
        public DateTime cat01created_date { get; set; }
        public DateTime cat01updated_date { get; set; }
        public ICollection<cat02menu_sub_category> sub_category { get; set; }


    }
}
