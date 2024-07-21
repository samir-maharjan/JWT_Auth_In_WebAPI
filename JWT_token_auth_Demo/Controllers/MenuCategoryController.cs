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
using System;
using System.Text.RegularExpressions;

namespace JWT_token_auth_Demo.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class MenuCategoryController : ControllerBase
    {

        private readonly AppDbContext _dbcontext;
        public MenuCategoryController(AppDbContext appDbContext)
        {
            _dbcontext = appDbContext;
        }

        [HttpPost("CreateMenuCategory")]
        public async Task<ActionResult<AfterSavedResponseVM>> CreateMenuCategory([FromForm] MenuCatVM menuCatVM)
        {
            try
            {
                if (menuCatVM != null)
                {
                    cat01menu_category category = new cat01menu_category();
                    category.cat01uin = Guid.NewGuid().ToString();
                    category.cat01category_code = menuCatVM.CategoryCode;
                    category.cat01category_title = menuCatVM.CategoryName;
                    category.cat01status = true;
                    category.cat01deleted = false;
                    category.cat01created_name = "Admin";
                    category.cat01updated_name = "Admin";
                    category.cat01created_date = DateTime.Now;
                    category.cat01updated_date = DateTime.Now;

                    if (menuCatVM.ThumbnailImgFile != null)
                    {

                        string uploadedImage = await UploadFile("CategoryThumbnailImages", menuCatVM.ThumbnailImgFile);
                        category.cat01thumbnail_img_path = uploadedImage;
                    }

                    _dbcontext.cat01menu_category.Add(category);
                    _dbcontext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return new AfterSavedResponseVM { status = false, error_msg = ex.ToString() };
                throw new Exception("Error:", ex);
            }
            return new AfterSavedResponseVM { status = true };

        }

        [HttpGet("CategoryList")]
        public async Task<IEnumerable<CategoryResponseVM>> CategoryList()
        {
            try
            {
                List<cat01menu_category> res = await _dbcontext.cat01menu_category.Where(x => !x.cat01deleted).ToListAsync();
                IList<CategoryResponseVM> resList = new List<CategoryResponseVM>();
                foreach (var item in res)
                {
                    CategoryResponseVM res1 = new CategoryResponseVM()
                    {
                        ID = item.cat01uin,
                        CategoryTitle = item.cat01category_title,
                        CategoryCode = item.cat01category_code,
                        ThumbnailImagePath = item.cat01thumbnail_img_path,
                        Status = item.cat01status,
                        Deleted = item.cat01deleted
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


        [HttpGet("UpdateMenuCategory")]
        public async Task<CategoryResponseVM> UpdateMenuCategory(string id)
        {
            try
            {
                cat01menu_category? catDetails = _dbcontext.cat01menu_category.Where(x => x.cat01uin == id).FirstOrDefault();
                if (catDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                CategoryResponseVM res1 = new CategoryResponseVM()
                {
                    ID = catDetails.cat01uin,
                    CategoryCode = catDetails.cat01category_code,
                    CategoryTitle = catDetails.cat01category_title,
                    ThumbnailImagePath = catDetails.cat01thumbnail_img_path == null ? "" : catDetails.cat01thumbnail_img_path,
                    Status = catDetails.cat01status,
                    Deleted = catDetails.cat01deleted
                };
                return res1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }


        [HttpPost("UpdateMenuCategory")]
        public async Task<ActionResult<AfterSavedResponseVM>> UpdateCategory([FromForm] MenuCatVM res)
        {
            try
            {
                cat01menu_category? catDetails = _dbcontext.cat01menu_category.Where(x => x.cat01uin == res.Id).FirstOrDefault();
                if (catDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                catDetails.cat01status = res.Status;
                catDetails.cat01category_code = res.CategoryCode;
                catDetails.cat01category_title = res.CategoryName;
                catDetails.cat01deleted = res.Deleted;
                catDetails.cat01updated_date = DateTime.Now;
                catDetails.cat01updated_name = "admin";

                if (res.ThumbnailImgFile != null)
                {
                    if (catDetails.cat01thumbnail_img_path != null)
                    {
                        DeleteProfileImg(catDetails.cat01thumbnail_img_path);
                    }
                    string newFilePath = await UploadFile("CategoryThumbnailImages", res.ThumbnailImgFile);
                    catDetails.cat01thumbnail_img_path = newFilePath;

                }

                _dbcontext.cat01menu_category.Update(catDetails);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new AfterSavedResponseVM { status = false, error_msg = ex.ToString() };
                throw new Exception("Error:", ex);
            }
            return new AfterSavedResponseVM { status = true };
        }

        private async Task<string> UploadFile(string folderName, IFormFile file)
        {
            try
            {
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                string fileExtension = Path.GetExtension(file!.FileName);

                if (Array.IndexOf(allowedExtensions, fileExtension.ToLower()) == -1)
                {
                    throw new InvalidOperationException("Invalid file! Only JPG, JPEG, and PNG files are allowed.");
                }

                string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                string yearMonthFolder = DateTime.Now.ToString("yyyy/MM");
                string uploadsFolder = Path.Combine(folderName, yearMonthFolder);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var image = Image.Load(file.OpenReadStream()))
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

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Store file information
                var uploadedImage = Path.Combine("~", folderName, yearMonthFolder, uniqueFileName).Replace("\\", "/");
                return uploadedImage;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while uploading the file.", ex);
            }
        }

        private IActionResult DeleteProfileImg(string relativePath)
        {
            try
            {
                var absolutePath = Path.Combine(relativePath.TrimStart('~').TrimStart('/'));
                if (System.IO.File.Exists(absolutePath))
                {
                    System.IO.File.Delete(absolutePath); // delete file from the file path
                    return Ok("File deleted successfully.");
                }
                return NotFound("File not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
