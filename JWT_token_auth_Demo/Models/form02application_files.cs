using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.Models
{
    public class form02application_files
    {
        [Key]
        public string form02uin { get; set; }
        public string form02form01uin { get; set; }
        public string form02img_path { get; set; }
        public string form02img_name { get; set; }
        public bool form02status { get; set; }
        public bool form02deleted { get; set; }
        public string form02created_name { get; set; }
        public string form02updated_name { get; set; }
        public DateTime form02created_date { get; set; }
        public DateTime form02updated_date { get; set; }
        public form01application form01application { get; set; }

    }
}
