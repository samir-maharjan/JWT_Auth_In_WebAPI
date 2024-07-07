using System.ComponentModel.DataAnnotations;
using static JWT_token_auth_Demo.Enum;

namespace JWT_token_auth_Demo.Models
{
    public class pro01product
    {
        [Key]
        public string pro01uin { get; set; }
        public string pro01name { get; set; }
        public string pro01code { get; set; }
        public string pro01cat01uin { get; set; }//category
        public string pro01cat02uin { get; set; }//sub_category
        public double pro01price { get; set; }
        public string pro01address { get; set; }
        public string pro01map_link { get; set; }
        public string pro01video_link { get; set; }
        public string pro01description { get; set; }
        public string pro01details { get; set; }
        public string? pro01thumbnail_img_path { get; set; }
        public int pro01room_count { get; set; }
        public int pro01bathroom_count { get; set; }
        public int pro01parking_count { get; set; }
        public double pro01area { get; set; }
        public EnumPropertyStatus pro01property_stats { get; set; }

        public bool pro01status { get; set; }
        public bool pro01deleted { get; set; }
        public string pro01created_name { get; set; }
        public string pro01updated_name { get; set; }
        public DateTime pro01created_date { get; set; }
        public DateTime pro01updated_date { get; set; }

        public ICollection<pro02product_files> pro02product_files { get; set; }


    }
}
