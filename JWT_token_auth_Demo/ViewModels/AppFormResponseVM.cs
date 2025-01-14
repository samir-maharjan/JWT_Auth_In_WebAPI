using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.ViewModels
{
    public class AppFormResponseVM
    {
        public AppFormResponseVM()
        {
            Images = new List<AppFormFiles>();
        }
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<AppFormFiles>? Images { get; set; }
        public List<IFormFile>? NewImgFile { get; set; }

    }

    public class AppFormFiles
    {
        public string? ImageId { get; set; }
        public string? Name { get; set; }
        public string? FilePath { get; set; }
        public DateTime? UploadedDate { get; set; }
    }
}
