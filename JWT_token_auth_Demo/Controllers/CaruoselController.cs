using JWT_token_auth_Demo.Data;
using JWT_token_auth_Demo.Models;
using JWT_token_auth_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                if (caruoselVM.ImgFile != null)
                {
                    foreach (var imgFile in caruoselVM.ImgFile)
                    {
                        string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                        string fileExtension = Path.GetExtension(imgFile!.FileName);

                        if (Array.IndexOf(allowedExtensions, fileExtension.ToLower()) == -1)
                        {
                            return BadRequest("Invalid file! Only JPG, JPEG, and PNG files are allowed.");
                        }

                        string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(imgFile!.FileName)}";
                        string yearMonthFolder = DateTime.Now.ToString("yyyy/MM");
                        string uploadsFolder = Path.Combine("CaruoselImages", yearMonthFolder);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imgFile!.CopyToAsync(stream);
                        }

                        // Store file information
                        var uploadedImage = ($"~/CaruoselImages/{yearMonthFolder}/{uniqueFileName}");

                        car01caruosel _img = new car01caruosel();
                        _img.car01uin = Guid.NewGuid().ToString();
                        _img.car01img_path = uploadedImage;
                        _img.car01img_name = imgFile!.FileName;
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
