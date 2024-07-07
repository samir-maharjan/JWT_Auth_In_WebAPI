using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWT_token_auth_Demo.ViewModels
{
    public class MenuCatVM
    {
        public string? Id { get; set; }
        public string CategoryCode { get; set; }
        public IFormFile? ThumbnailImgFile { get; set; }
        public string CategoryName { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
    }

    public class MenuSubCatVM
    {
        public string? Id { get; set; }
        public string CategoryID { get; set; }
        public string SubCategoryCode { get; set; }
        public IFormFile? ThumbnailImgFile { get; set; }
        public string SubCategoryName { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public string CategoryId { get; set; }
    }
}
