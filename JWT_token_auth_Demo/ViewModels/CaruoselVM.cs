using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.ViewModels
{
    public class CaruoselVM
    {   
        public List<CaruoselDetailsVM>? caruoselDetailsVM { get; set; }


    }

    public class CaruoselDetailsVM
    {
        public string? CaruoselTitle { get; set; }
        public string? CaruoselDescription { get; set; }
        public string? CaruoselLink { get; set; }
        public IFormFile? ImgFile { get; set; }
    }
}
