using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Models
{
    public class usr01users
    {
        [Key]
        public string usr01uin { get; set; }
        public string usr01user_name { get; set; }
        public string usr01first_name { get; set; }
        public string usr01last_name { get; set; }
        public string usr01occupation { get; set; }
        public string usr01post { get; set; }
        public string usr01address { get; set; }
        public string usr01contact_number { get; set; }
        public string usr01email { get; set; }

        public string usr01created_name { get; set; }
        public string usr01updated_name { get; set; }
        public DateTime usr01created_date { get; set; }
        public DateTime usr01updated_date { get; set; }
        //will work on the role later, there may be many role but for now let it be string
        public string usr01reg_role { get; set; }
        public bool can_view_all_data { get; set; }
        public bool can_view_all_department { get; set; }
        public bool usr01status { get; set; }
        public bool usr01deleted { get; set; }
        public bool usr01approved { get; set; }
        public string? usr01profile_img_path { get; set; }

    }
}
