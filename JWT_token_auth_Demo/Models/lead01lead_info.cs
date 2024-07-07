using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Models
{
    public class lead01lead_info
    {
        [Key]
        public string lead01uin { get; set; }
        public string lead0first_name { get; set; }
        public string lead0middle_name { get; set; }
        public string lead0last_name { get; set; }
        public string lead01address { get; set; }
        public string lead01phone_number { get; set; }
        public string lead01email { get; set; }
        public string lead01property { get; set; }
        public string lead01category { get; set; }
        public string lead01agent { get; set; }
        public string lead01query_message { get; set; }
        public bool lead01status { get; set; }
        public bool lead01deleted { get; set; }
        public string lead01created_name { get; set; }
        public string lead01updated_name { get; set; }
        public DateTime lead01created_date { get; set; }
        public DateTime lead01updated_date { get; set; }

    }
}
