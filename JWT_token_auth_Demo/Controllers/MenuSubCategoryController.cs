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
    public class MenuSubCategoryController : ControllerBase
    {

        private readonly AppDbContext _dbcontext;
        public MenuSubCategoryController(AppDbContext appDbContext)
        {
            _dbcontext = appDbContext;
        }

        [HttpPost("CreateSubMenuCategory")]
        public async Task<ActionResult<AfterSavedResponseVM>> CreateSubMenuCategory([FromForm] MenuSubCatVM menuCatVM)
        {
            try
            {
                if (menuCatVM != null)
                {
                    cat02menu_sub_category category = new cat02menu_sub_category();
                    category.cat02uin = Guid.NewGuid().ToString();
                    category.cat02cat01uin = menuCatVM.CategoryId;
                    category.cat02sub_category_code = menuCatVM.SubCategoryCode;
                    category.cat02sub_category_title = menuCatVM.SubCategoryName;
                    category.cat02status = true;
                    category.cat02deleted = false;
                    category.cat02created_name = "Admin";
                    category.cat02updated_name = "Admin";
                    category.cat02created_date = DateTime.Now;
                    category.cat02updated_date = DateTime.Now;

                    if (menuCatVM.ThumbnailImgFile != null)
                    {

                        string uploadedImage = await UploadFile("SubCategoryThumbnailImages", menuCatVM.ThumbnailImgFile);
                        category.cat02thumbnail_img_path = uploadedImage;
                    }

                    _dbcontext.cat02menu_sub_category.Add(category);
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

        [HttpGet("SubCategoryList")]
        public async Task<IEnumerable<SubCategoryResponseVM>> SubCategoryList()
        {
            try
            {
                List<cat02menu_sub_category> res = await _dbcontext.cat02menu_sub_category.Where(x => !x.cat02deleted).Include(x => x.cat01menu_category).ToListAsync();
                IList<SubCategoryResponseVM> resList = new List<SubCategoryResponseVM>();
                foreach (var item in res)
                {
                    SubCategoryResponseVM res1 = new SubCategoryResponseVM()
                    {
                        ID = item.cat02uin,
                        CategoryID = item.cat02cat01uin,
                        CategoryTitle = item.cat01menu_category.cat01category_title,
                        SubCategoryTitle = item.cat02sub_category_title,
                        SubCategoryCode = item.cat02sub_category_code,
                        ThumbnailImagePath = item.cat02thumbnail_img_path,
                        Status = item.cat02status,
                        Deleted = item.cat02deleted
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

        [HttpGet("SubCategoryListWithCategoryID")]
        public async Task<IEnumerable<SubCategoryResponseVM>> SubCategoryListWithCategoryID(string id)
        {
            try
            {
                List<cat02menu_sub_category> res = await _dbcontext.cat02menu_sub_category.Where(x => x.cat02cat01uin == id && !x.cat02deleted).Include(x => x.cat01menu_category).ToListAsync();
                IList<SubCategoryResponseVM> resList = new List<SubCategoryResponseVM>();
                foreach (var item in res)
                {
                    SubCategoryResponseVM res1 = new SubCategoryResponseVM()
                    {
                        ID = item.cat02uin,
                        CategoryID = item.cat02cat01uin,
                        CategoryTitle = item.cat01menu_category.cat01category_title,
                        SubCategoryTitle = item.cat02sub_category_title,
                        SubCategoryCode = item.cat02sub_category_code,
                        Status = item.cat02status,
                        Deleted = item.cat02deleted
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

        [HttpGet("UpdateSubMenuCategory")]
        public async Task<SubCategoryResponseVM> UpdateSubMenuCategory(string id)
        {
            try
            {
                cat02menu_sub_category? res = _dbcontext.cat02menu_sub_category.Where(x => x.cat02uin == id).Include(x => x.cat01menu_category).FirstOrDefault();
                if (res == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                SubCategoryResponseVM res1 = new SubCategoryResponseVM()
                {
                    ID = res.cat02uin,
                    SubCategoryCode = res.cat02sub_category_code,
                    SubCategoryTitle = res.cat02sub_category_title,
                    CategoryID = res.cat02cat01uin,
                    CategoryTitle = res.cat01menu_category.cat01category_title,
                    ThumbnailImagePath = res.cat02thumbnail_img_path,
                    Status = res.cat02status,
                    Deleted = res.cat02deleted
                };
                return res1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }


        [HttpPost("UpdateSubCategory")]
        public async Task<ActionResult<AfterSavedResponseVM>> UpdateSubCategory([FromForm] MenuSubCatVM res)
        {
            try
            {
                cat02menu_sub_category? catDetails = _dbcontext.cat02menu_sub_category.Where(x => x.cat02uin == res.Id).Include(x => x.cat01menu_category).FirstOrDefault();
                if (catDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                catDetails.cat02sub_category_code = res.SubCategoryCode;
                catDetails.cat02sub_category_title = res.SubCategoryName;
                catDetails.cat02cat01uin = res.CategoryID;
                catDetails.cat02status = res.Status;
                catDetails.cat02deleted = res.Deleted;
                catDetails.cat02updated_date = DateTime.Now;
                catDetails.cat02updated_name = "admin";
                if (res.ThumbnailImgFile != null)
                {
                    if (catDetails.cat02thumbnail_img_path != null)
                    {
                        DeleteProfileImg(catDetails.cat02thumbnail_img_path);
                    }
                    string newFilePath = await UploadFile("SubCategoryThumbnailImages", res.ThumbnailImgFile);
                    catDetails.cat02thumbnail_img_path = newFilePath;
                }
                _dbcontext.cat02menu_sub_category.Update(catDetails);
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

                /*  using (var stream = new FileStream(filePath, FileMode.Create))
                  {
                      await file.CopyToAsync(stream);
                  }*/

                using (var originalImageStream = file.OpenReadStream())
                {
                    using (var image = Image.Load(originalImageStream))
                    {
                        // Resize the image (optional)
                        /*image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Max,
                            Size = new Size(800, 600) // Adjust dimensions as needed
                        }));*/

                        // Save the compressed image to a memory stream
                        using (var memoryStream = new MemoryStream())
                        {
                            image.Save(memoryStream, new JpegEncoder
                            {
                                Quality = 75 // Adjust quality as needed
                            });

                            // Compare sizes
                            if (memoryStream.Length < originalImageStream.Length)
                            {
                                // Save compressed image to file
                                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                                {
                                    memoryStream.Position = 0;
                                    await memoryStream.CopyToAsync(fileStream);
                                }
                            }
                            else
                            {
                                // Save original image to file if it's smaller or the same size
                                originalImageStream.Position = 0; // Reset stream position
                                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                                {
                                    await originalImageStream.CopyToAsync(fileStream);
                                }
                            }
                        }
                    }
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
