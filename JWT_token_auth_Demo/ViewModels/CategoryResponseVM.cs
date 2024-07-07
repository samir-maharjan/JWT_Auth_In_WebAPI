using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.ViewModels
{
    public class CategoryResponseVM
    {
        public string ID { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryCode { get; set; }
        public string ThumbnailImagePath { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }

    }
    public class SubCategoryResponseVM
    {
        public string ID { get; set; }
        public string CategoryID { get; set; }
        public string CategoryTitle { get; set; }
        public string SubCategoryTitle { get; set; }
        public string SubCategoryCode { get; set; }
        public string ThumbnailImagePath { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
    }

    public class SubCategoryVM
    {
        public string ID { get; set; }
        public string CategoryID { get; set; }
        public string SubCategoryTitle { get; set; }
        public string SubCategoryCode { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
    }
}
