using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.ViewModels
{
    public class AgentVM
    {
        public string AgentCode { get; set; }
        public string AgentName { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string Experience { get; set; }
        public string Skill { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Description { get; set; }
        public string FBLink { get; set; }
        public string WebsiteLink { get; set; }
        public string LinkedInLink { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public AgentFileVM AgentFile { get; set; }

    }

    public class AgentFileVM
    {
        public List<IFormFile>? ImgFile { get; set; }

    }
}
