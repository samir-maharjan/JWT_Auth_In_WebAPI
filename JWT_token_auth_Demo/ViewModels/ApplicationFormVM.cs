using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.ViewModels
{
    public class ApplicationFormVM
    {
        public string? ID { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName{ get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public string? ContactNumber { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ApplicationFormFileVM? ApplicationFormFile { get; set; }

    }

    public class ApplicationFormSearchVM
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public string? ContactNumber { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }

    public class ApplicationFormFileVM
    {
        public List<IFormFile>? ImgFile { get; set; }

    }
}
