using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.ViewModels
{
    public class AgentResponseVM
    {
/*        public AgentResponseVM()
        {
            Images = new List<AgentImgFiles>();
        }*/
        public string ID { get; set; }
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
        public string AgentImgPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
