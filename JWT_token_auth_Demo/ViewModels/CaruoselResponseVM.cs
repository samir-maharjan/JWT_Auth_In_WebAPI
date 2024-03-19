using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.ViewModels
{
    public class CaruoselResponseVM
    {
        public string ID { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }

    }
}
