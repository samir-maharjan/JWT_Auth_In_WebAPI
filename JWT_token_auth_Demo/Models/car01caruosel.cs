using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Models
{
    public class car01caruosel
    {
        [Key]
        public string car01uin { get; set; }
        public string car01img_path { get; set; }
        public string car01img_name { get; set; }
        public string car01title { get; set; }
        public string car01description { get; set; }
        public string car01link { get; set; }
        public bool car01status { get; set; }
        public bool car01deleted { get; set; }
        public string car01created_name { get; set; }
        public string car01updated_name { get; set; }
        public DateTime car01created_date { get; set; }
        public DateTime car01updated_date { get; set; }

    }
}
