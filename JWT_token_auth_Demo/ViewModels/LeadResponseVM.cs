using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWT_token_auth_Demo.ViewModels
{
    public class LeadResponseVM
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string Property { get; set; }
        public string CategoryId { get; set; }
        public string AgentId { get; set; }
        public string QueryMessgae { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
    }
}
