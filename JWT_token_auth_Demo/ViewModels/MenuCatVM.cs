using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWT_token_auth_Demo.ViewModels
{
    public class MenuCatVM
    {
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
    }

    public class MenuSubCatVM
    {
        public MenuCatVM CatDetails { get; set; }
        public string CategoryId { get; set; }
    }
}
