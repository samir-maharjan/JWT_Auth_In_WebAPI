using JWT_token_auth_Demo.Data;
using JWT_token_auth_Demo.Models;
using JWT_token_auth_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Text.RegularExpressions;

namespace JWT_token_auth_Demo.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class CaruoselController : ControllerBase
    {

        private readonly AppDbContext _dbcontext;
        public CaruoselController(AppDbContext appDbContext)
        {
            _dbcontext = appDbContext;
        }

        [HttpPost("UploadCaruoselImage")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<AfterSavedResponseVM>> UploadCaruoselImage([FromForm] CaruoselVM caruoselVM)
        {
            try
            {
                if (caruoselVM.caruoselDetailsVM != null && caruoselVM.caruoselDetailsVM.Count > 0)
                {
                    foreach (var imgFile in caruoselVM.caruoselDetailsVM)
                    {
                        string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                        string fileExtension = Path.GetExtension(imgFile!.ImgFile.FileName);

                        if (Array.IndexOf(allowedExtensions, fileExtension.ToLower()) == -1)
                        {
                            return BadRequest("Invalid file! Only JPG, JPEG, and PNG files are allowed.");
                        }

                        string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(imgFile!.ImgFile.FileName)}";
                        string yearMonthFolder = DateTime.Now.ToString("yyyy/MM");
                        string uploadsFolder = Path.Combine("CaruoselImages", yearMonthFolder);

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        /*using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imgFile!.ImgFile.CopyToAsync(stream);
                        }*/

                        using (var image = Image.Load(imgFile.ImgFile.OpenReadStream()))
                        {
                            // Resize the image (optional)
                           /* image.Mutate(x => x.Resize(new ResizeOptions
                            {
                                Mode = ResizeMode.Max,
                                Size = new Size(800, 600) // Adjust dimensions as needed
                            }));*/

                            // Save the compressed image
                            await image.SaveAsync(filePath, new JpegEncoder
                            {
                                Quality = 50 // Adjust quality as needed
                            });
                        }


                        // Store file information
                        var uploadedImage = ($"~/CaruoselImages/{yearMonthFolder}/{uniqueFileName}");

                        car01caruosel _img = new car01caruosel();
                        _img.car01uin = Guid.NewGuid().ToString();
                        _img.car01title = imgFile.CaruoselTitle !=null? imgFile.CaruoselTitle:"";
                        _img.car01description = imgFile.CaruoselDescription != null ? imgFile.CaruoselDescription : "";
                        _img.car01link = imgFile.CaruoselLink != null ? imgFile.CaruoselLink : "";
                        _img.car01img_path = uploadedImage;
                        _img.car01img_name = imgFile!.ImgFile.FileName;
                        _img.car01status = true;
                        _img.car01deleted = false;
                        _img.car01created_date = DateTime.Now;
                        _img.car01updated_date = DateTime.Now;
                        _img.car01created_name = "admin";
                        _img.car01updated_name = "admin";

                        _dbcontext.car01caruosel.Add(_img);
                        _dbcontext.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                return new AfterSavedResponseVM { status = false, error_msg = ex.ToString() };
                throw new Exception("Error:", ex);
            }
            return new AfterSavedResponseVM { status = true };

        }

        [HttpGet("CaruoselList")]
        public async Task<IEnumerable<CaruoselResponseVM>> CaruoselList()
        {
            try
            {
                List<car01caruosel> res = await _dbcontext.car01caruosel.Where(x => !x.car01deleted).ToListAsync();
                IList<CaruoselResponseVM> resList = new List<CaruoselResponseVM>();
                foreach (var item in res)
                {
                    CaruoselResponseVM res1 = new CaruoselResponseVM()
                    {
                        ID = item.car01uin,
                        CaruoselDescription = item.car01description,
                        CaruoselTitle = item.car01title,
                        CaruoselLink = item.car01link,
                        ImagePath = item.car01img_path,
                        ImageName = item.car01img_name,
                        Status = item.car01status,
                        Deleted = item.car01deleted
                    };
                    resList.Add(res1);
                }

                return resList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }

           
        }

        [HttpGet("CaruoselInfoUpdate")]
        public async Task<CaruoselResponseVM> CaruoselInfoUpdate(string id)
        {
            try
            {
                car01caruosel imgDetails = _dbcontext.car01caruosel.Where(x => x.car01uin == id).FirstOrDefault();
                if (imgDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                CaruoselResponseVM res1 = new CaruoselResponseVM()
                {
                    ID = imgDetails.car01uin,
                    ImagePath = imgDetails.car01img_path,
                    CaruoselDescription = imgDetails.car01description,
                    CaruoselTitle = imgDetails.car01title,
                    CaruoselLink = imgDetails.car01link,
                    ImageName = imgDetails.car01img_name,
                    Status = imgDetails.car01status,
                    Deleted = imgDetails.car01deleted
                };
                return res1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }


        [HttpPost("CaruoselInfoUpdate")]
        public async Task<ActionResult<AfterSavedResponseVM>> CaruoselUpdate(CaruoselResponseVM res)
        {
            try
            {
                car01caruosel imgDetails = _dbcontext.car01caruosel.Where(x => x.car01uin == res.ID).FirstOrDefault();
                if (imgDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                imgDetails.car01title = res.CaruoselTitle;
                imgDetails.car01description = res.CaruoselDescription;
                imgDetails.car01link = res.CaruoselLink;
                imgDetails.car01status = res.Status;
                imgDetails.car01deleted = res.Deleted;
                imgDetails.car01updated_date = DateTime.Now;
                imgDetails.car01updated_name = "admin";

                _dbcontext.car01caruosel.Update(imgDetails);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new AfterSavedResponseVM { status = false, error_msg = ex.ToString() };
                throw new Exception("Error:", ex);
            }
            return new AfterSavedResponseVM { status = true };
        }
    }
}
