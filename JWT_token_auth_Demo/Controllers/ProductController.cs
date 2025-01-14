using JWT_token_auth_Demo.Data;
using JWT_token_auth_Demo.Models;
using JWT_token_auth_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFormCore.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using static JWT_token_auth_Demo.Enum;

namespace JWT_token_auth_Demo.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ProductController : ControllerBase
    {

        private readonly AppDbContext _dbcontext;
        public ProductController(AppDbContext appDbContext)
        {
            _dbcontext = appDbContext;
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult<AfterSavedResponseVM>> CreateProduct([FromForm] ProductVM productVM)
        {
            try
            {
                if (productVM != null)
                {
                    pro01product product = new pro01product();
                    product.pro01uin = Guid.NewGuid().ToString();
                    product.pro01name = productVM.ProductName;
                    product.pro01code = productVM.ProductCode;
                    product.pro01address = productVM.Address;
                    product.pro01price = productVM.Price;
                    product.pro01cat01uin = productVM.CategoryId;
                    product.pro01cat02uin = productVM.SubCategoryId;
                    product.pro01map_link = productVM.MapLink;
                    product.pro01video_link = productVM.VideoLink;
                    product.pro01description = productVM.Description;
                    product.pro01details = productVM.Details;
                    product.pro01room_count = productVM.RoomCount;
                    product.pro01bathroom_count = productVM.BathRoomCount;
                    product.pro01parking_count = productVM.ParkingCount;
                    product.pro01area = productVM.Area;
                    product.pro01status = true;
                    product.pro01property_stats = (EnumPropertyStatus)productVM.PropertyStatus;
                    product.pro01deleted = false;

                    if (productVM.ThumbnailImg != null)
                    {
                        string uploadedPath = await UploadFile("ProductThumbnail", productVM.ThumbnailImg);
                        product.pro01thumbnail_img_path = uploadedPath;
                    }

                    product.pro01created_name = "Admin";
                    product.pro01updated_name = "Admin";
                    product.pro01created_date = DateTime.Now;
                    product.pro01updated_date = DateTime.Now;
                    _dbcontext.pro01product.Add(product);

                    if (productVM.ProductFiles != null)
                    {
                        foreach (var imgFile in productVM.ProductFiles.ImgFile)
                        {

                            string uploadedPath = await UploadFile("ProductImages", imgFile);

                            pro02product_files _img = new pro02product_files();
                            _img.pro02uin = Guid.NewGuid().ToString();
                            _img.pro02pro01uin = product.pro01uin;
                            _img.pro02img_path = uploadedPath;
                            _img.pro02img_name = imgFile!.FileName;
                            _img.pro02status = true;
                            _img.pro02deleted = false;
                            _img.pro02created_date = DateTime.Now;
                            _img.pro02updated_date = DateTime.Now;
                            _img.pro02created_name = "admin";
                            _img.pro02updated_name = "admin";

                            _dbcontext.pro02product_files.Add(_img);
                        }
                    }
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

        [HttpGet("ProductList")]
        public async Task<IEnumerable<ProductResponseVM>> ProductList()
        {
            try
            {
               // List<pro01product> res = await _dbcontext.pro01product.Where(x => !x.pro01deleted).ToListAsync();
                List<pro01product> res = await _dbcontext.pro01product
                                                .Where(x => !x.pro01deleted)
                                                .OrderByDescending(x => x.pro01created_date)
                                                .Take(100)
                                                .ToListAsync();
                IList<ProductResponseVM> resList = new List<ProductResponseVM>();
                foreach (var item in res)
                {
                    ProductResponseVM res1 = new ProductResponseVM()
                    {
                        ID = item.pro01uin,
                        ProductName = item.pro01name,
                        ProductCode = item.pro01code,
                        CategoryId = item.pro01cat01uin,
                        SubCategoryId = item.pro01cat02uin,
                        Price = item.pro01price,
                        Address = item.pro01address,
                        MapLink = item.pro01map_link,
                        VideoLink = item.pro01video_link,
                        ThumbnailImgPath = item.pro01thumbnail_img_path == null ? "" : item.pro01thumbnail_img_path,
                        Description = item.pro01description,
                        Details = item.pro01details,
                        PropertyStatus = (int)item.pro01property_stats,
                        PropertyStatusValue = item.pro01property_stats.ToString(),
                        RoomCount = item.pro01room_count,
                        BathRoomCount = item.pro01bathroom_count,
                        ParkingCount = item.pro01parking_count,
                        Area = item.pro01area,
                        Status = item.pro01status,
                        Deleted = item.pro01deleted,
                        CreatedDate = item.pro01created_date,
                        UpdatedDate = item.pro01updated_date
                    };
                    List<pro02product_files> images = _dbcontext.pro02product_files.Where(x => x.pro02pro01uin == item.pro01uin && !x.pro02deleted && x.pro02status).ToList();

                    if (images.Count != 0)
                    {
                        foreach (var image in images)
                        {
                            res1.Images.Add(new ProductFiles
                            {
                                ImageId = image.pro02uin,
                                Name = image.pro02img_name,
                                FilePath = image.pro02img_path,
                                UploadedDate = image.pro02updated_date
                            });
                        }
                    }
                    resList.Add(res1);
                }

                return resList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }

        [HttpPost("FilterProductList")]
        public async Task<IEnumerable<ProductResponseVM>> FilterProductList([FromForm] ProductSearchVM searchParams)
        {
            try
            {
                var query = _dbcontext.pro01product.AsQueryable();

                // Apply filters based on search parameters
                if (!string.IsNullOrEmpty(searchParams.CategoryId))
                    query = query.Where(x => x.pro01cat01uin == searchParams.CategoryId);

                if (!string.IsNullOrEmpty(searchParams.SubCategoryId))
                    query = query.Where(x => x.pro01cat02uin == searchParams.SubCategoryId);

                if (searchParams.RoomCount.HasValue)
                    query = query.Where(x => x.pro01room_count == searchParams.RoomCount.Value);

                if (searchParams.BathRoomCount.HasValue)
                    query = query.Where(x => x.pro01bathroom_count == searchParams.BathRoomCount.Value);

                if (searchParams.ParkingCount.HasValue)
                    query = query.Where(x => x.pro01parking_count == searchParams.ParkingCount.Value);

                if (searchParams.MinPrice.HasValue)
                    query = query.Where(x => x.pro01price >= searchParams.MinPrice.Value);

                if (searchParams.MaxPrice.HasValue)
                    query = query.Where(x => x.pro01price <= searchParams.MaxPrice.Value);

                if (searchParams.MinArea.HasValue)
                    query = query.Where(x => x.pro01area >= searchParams.MinArea.Value);

                if (searchParams.MaxArea.HasValue)
                    query = query.Where(x => x.pro01area <= searchParams.MaxArea.Value);

                // Fetch filtered results
                var res = await query
                    .Where(x => !x.pro01deleted)
                    .OrderByDescending(x => x.pro01created_date)
                    .Take(100)
                    .ToListAsync();

                // Transform the results into response view model
                IList<ProductResponseVM> resList = new List<ProductResponseVM>();
                foreach (var item in res)
                {
                    var res1 = new ProductResponseVM
                    {
                        ID = item.pro01uin,
                        ProductName = item.pro01name,
                        ProductCode = item.pro01code,
                        CategoryId = item.pro01cat01uin,
                        SubCategoryId = item.pro01cat02uin,
                        Price = item.pro01price,
                        Address = item.pro01address,
                        MapLink = item.pro01map_link,
                        VideoLink = item.pro01video_link,
                        ThumbnailImgPath = item.pro01thumbnail_img_path ?? string.Empty,
                        Description = item.pro01description,
                        Details = item.pro01details,
                        PropertyStatus = (int)item.pro01property_stats,
                        PropertyStatusValue = item.pro01property_stats.ToString(),
                        RoomCount = item.pro01room_count,
                        BathRoomCount = item.pro01bathroom_count,
                        ParkingCount = item.pro01parking_count,
                        Area = item.pro01area,
                        Status = item.pro01status,
                        Deleted = item.pro01deleted,
                        CreatedDate = item.pro01created_date,
                        UpdatedDate = item.pro01updated_date
                    };

                    var images = _dbcontext.pro02product_files
                        .Where(x => x.pro02pro01uin == item.pro01uin && !x.pro02deleted && x.pro02status)
                        .ToList();

                    foreach (var image in images)
                    {
                        res1.Images.Add(new ProductFiles
                        {
                            ImageId = image.pro02uin,
                            Name = image.pro02img_name,
                            FilePath = image.pro02img_path,
                            UploadedDate = image.pro02updated_date
                        });
                    }

                    resList.Add(res1);
                }

                return resList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }

        [HttpGet("ProductListWithCategoryID")]
        public async Task<IEnumerable<ProductResponseVM>> ProductListWithCategoryID(string id)
        {
            try
            {
                List<pro01product> res = await _dbcontext.pro01product.Where(x => x.pro01cat01uin == id && !x.pro01deleted).Include(x => x.pro02product_files).ToListAsync();
                IList<ProductResponseVM> resList = ReturnProductList(res);
                return resList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }

        }


        [HttpGet("ProductListWithSubCategoryID")]
        public async Task<IEnumerable<ProductResponseVM>> ProductListWithSubCategoryID(string id)
        {
            try
            {
                List<pro01product> res = await _dbcontext.pro01product.Where(x => x.pro01cat02uin == id && !x.pro01deleted).Include(x => x.pro02product_files).ToListAsync();

                IList<ProductResponseVM> resList = ReturnProductList(res);
                return resList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }


        }

        private IList<ProductResponseVM> ReturnProductList(List<pro01product> res)
        {
            IList<ProductResponseVM> resList = new List<ProductResponseVM>();
            foreach (var item in res)
            {
                ProductResponseVM res1 = new ProductResponseVM()
                {
                    ID = item.pro01uin,
                    ProductName = item.pro01name,
                    ProductCode = item.pro01code,
                    CategoryId = item.pro01cat01uin,
                    SubCategoryId = item.pro01cat02uin,
                    Price = item.pro01price,
                    Address = item.pro01address,
                    MapLink = item.pro01map_link,
                    VideoLink = item.pro01video_link,
                    ThumbnailImgPath = item.pro01thumbnail_img_path == null ? "" : item.pro01thumbnail_img_path,
                    Description = item.pro01description,
                    Details = item.pro01details,
                    PropertyStatus = (int)item.pro01property_stats,
                    PropertyStatusValue = item.pro01property_stats.ToString(),
                    RoomCount = item.pro01room_count,
                    BathRoomCount = item.pro01bathroom_count,
                    ParkingCount = item.pro01parking_count,
                    Area = item.pro01area,
                    Status = item.pro01status,
                    Deleted = item.pro01deleted,
                    CreatedDate = item.pro01created_date,
                    UpdatedDate = item.pro01updated_date
                };

                List<pro02product_files> images = _dbcontext.pro02product_files.Where(x => x.pro02pro01uin == item.pro01uin && !x.pro02deleted && x.pro02status).ToList();


                if (images.Count != 0)
                {
                    foreach (var image in images)
                    {
                        ProductFiles img = new ProductFiles()
                        {
                            ImageId = image.pro02uin,
                            Name = image.pro02img_name,
                            FilePath = image.pro02img_path,
                            UploadedDate = image.pro02updated_date
                        };
                        res1.Images.Add(img);
                    }
                }
                resList.Add(res1);
            }
            return resList;
        }
        [HttpGet("PropertyStatusList")]
        public async Task<IEnumerable> PropertyStatusList()
        {
            return EnumHelper.GetEnumList<EnumPropertyStatus>().ToList();
        }


        [HttpGet("UpdateProduct")]
        public async Task<ProductResponseVM> UpdateProduct(string id)
        {
            try
            {
                pro01product product = _dbcontext.pro01product.Where(x => x.pro01uin == id).FirstOrDefault();
                if (product == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                ProductResponseVM res1 = new ProductResponseVM()
                {
                    ID = product.pro01uin,
                    ProductName = product.pro01name,
                    ProductCode = product.pro01code,
                    CategoryId = product.pro01cat01uin,
                    SubCategoryId = product.pro01cat02uin,
                    Price = product.pro01price,
                    Address = product.pro01address,
                    MapLink = product.pro01map_link,
                    VideoLink = product.pro01video_link,
                    NewThumbnailImg = null,
                    ThumbnailImgPath = product.pro01thumbnail_img_path == null ? "" : product.pro01thumbnail_img_path,
                    Description = product.pro01description,
                    Details = product.pro01details,
                    PropertyStatus = (int?)(EnumPropertyStatus)product.pro01property_stats,
                    PropertyStatusValue = product.pro01property_stats.ToString(),
                    RoomCount = product.pro01room_count,
                    BathRoomCount = product.pro01bathroom_count,
                    ParkingCount = product.pro01parking_count,
                    Area = product.pro01area,
                    Status = product.pro01status,
                    Deleted = product.pro01deleted,
                    CreatedDate = product.pro01created_date,
                    UpdatedDate = product.pro01updated_date
                };

                List<pro02product_files> images = _dbcontext.pro02product_files.Where(x => x.pro02pro01uin == product.pro01uin && !x.pro02deleted && x.pro02status).ToList();


                if (images.Count != 0)
                {
                    foreach (var image in images)
                    {
                        ProductFiles img = new ProductFiles()
                        {
                            ImageId = image.pro02uin,
                            Name = image.pro02img_name,
                            FilePath = image.pro02img_path,
                            UploadedDate = image.pro02updated_date
                        };
                        res1.Images.Add(img);
                    }
                }
                return res1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }


        [HttpPost("UpdateProductDes")]
        public async Task<ActionResult<AfterSavedResponseVM>> UpdateProductDes([FromForm] ProductResponseVM res)
        {
            try
            {
                if (string.IsNullOrEmpty(res.ID) || string.IsNullOrWhiteSpace(res.ID))
                {
                    throw new ArgumentException("ID cannot be null or empty.");
                }

                pro01product proDetails = _dbcontext.pro01product.Where(x => x.pro01uin == res.ID).FirstOrDefault();

                if (proDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }

                proDetails.pro01status = (bool)res.Status;
                proDetails.pro01code = res.ProductCode;
                proDetails.pro01name = res.ProductName;
                proDetails.pro01cat01uin = res.CategoryId;
                proDetails.pro01cat02uin = res.SubCategoryId;
                proDetails.pro01price = (double)res.Price;
                proDetails.pro01area = (double)res.Area;
                proDetails.pro01map_link = res.MapLink;
                proDetails.pro01video_link = res.VideoLink;
                proDetails.pro01address = res.Address;
                proDetails.pro01property_stats = (EnumPropertyStatus)res.PropertyStatus;
                proDetails.pro01description = res.Description;
                proDetails.pro01details = res.Details;
                proDetails.pro01room_count = (int)res.RoomCount;
                proDetails.pro01bathroom_count = (int)res.BathRoomCount;
                proDetails.pro01parking_count = (int)res.ParkingCount;
                proDetails.pro01deleted = (bool)res.Deleted;
                proDetails.pro01updated_date = DateTime.Now;
                proDetails.pro01updated_name = "admin";

                if (res.NewThumbnailImg != null)
                {
                    if (proDetails.pro01thumbnail_img_path != null)
                    {
                        DeleteThumbnailImg(proDetails.pro01thumbnail_img_path);
                    }
                    string newFilePath = await UploadFile("ProductThumbnail", res.NewThumbnailImg);
                    proDetails.pro01thumbnail_img_path = newFilePath;

                }
                /*                else
                                {
                                    proDetails.pro01thumbnail_img_path = res.ThumbnailImgPath == null ? "" : res.ThumbnailImgPath;

                                }*/

                // Load Images id that have been fetched from parameter in an array list old_files_param
                var old_files_param = res.Images.Where(pf => !string.IsNullOrEmpty(pf.ImageId)).Select(x => x.ImageId).ToList();

                //load old images values from database and save in an array list => old_files_DB

                List<pro02product_files> old_files_DB = _dbcontext.pro02product_files.Where(x => x.pro02pro01uin == res.ID && !x.pro02deleted && x.pro02status).ToList();

                // Compare old files from database with fetched ids and update status for missing ids
                foreach (var oldProductFile in old_files_DB)
                {
                    if (!old_files_param.Contains(oldProductFile.pro02uin))
                    {
                        oldProductFile.pro02status = false;
                        oldProductFile.pro02deleted = true;
                    }
                }
                if (res.NewImgFile != null && res.NewImgFile.Any())
                {
                    foreach (var imgFile in res.NewImgFile)
                    {
                        if (imgFile == null)
                        {
                            continue; // Skip processing if imgFile is null
                        }

                        string uploadedPath = await UploadFile("ProductImages", imgFile);

                        pro02product_files _img = new pro02product_files();
                        _img.pro02uin = Guid.NewGuid().ToString();
                        _img.pro02pro01uin = res.ID;
                        _img.pro02img_path = uploadedPath;
                        _img.pro02img_name = imgFile!.FileName;
                        _img.pro02status = true;
                        _img.pro02deleted = false;
                        _img.pro02created_date = DateTime.Now;
                        _img.pro02updated_date = DateTime.Now;
                        _img.pro02created_name = "admin";
                        _img.pro02updated_name = "admin";

                        _dbcontext.pro02product_files.Add(_img);
                    }
                }
                    // Add new images from new image file
                    
                _dbcontext.pro02product_files.UpdateRange(old_files_DB);
                _dbcontext.pro01product.Update(proDetails);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new AfterSavedResponseVM { status = false, error_msg = ex.ToString() };
                throw new Exception("Error:", ex);
            }
            return new AfterSavedResponseVM { status = true };
        }

        [HttpPost("DeleteProduct")]
        public async Task<ActionResult<AfterSavedResponseVM>> DeleteProduct(string id)
        {
            try
            {
                if (id != null)
                {
                    pro01product _product = _dbcontext.pro01product.Where(x => x.pro01uin == id).FirstOrDefault();

                    _product.pro01status = false;
                    _product.pro01deleted = true;
                    _dbcontext.pro01product.Update(_product);
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

        private IActionResult DeleteThumbnailImg(string relativePath)
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


/*[HttpDelete("delete/{fileName}")]
public IActionResult DeleteFile(string fileName)
{
    try
    {
        var filePath = Path.Combine(_uploadDirectory, fileName);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath); //delete file from the file path
            return Ok("File deleted successfully.");
        }
        return NotFound("File not found.");
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}*/