﻿using JWT_token_auth_Demo.Data;
using JWT_token_auth_Demo.Models;
using JWT_token_auth_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFormCore.Helpers;
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
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProductController(AppDbContext appDbContext, IWebHostEnvironment hostingEnvironment)
        {
            _dbcontext = appDbContext;
            _hostingEnvironment = hostingEnvironment;
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
                    product.pro01area = productVM.Area;
                    product.pro01status = productVM.Status;
                    product.pro01property_stats = (EnumPropertyStatus)productVM.PropertyStatus;
                    product.pro01deleted = productVM.Deleted;

                    if (productVM.ThumbnailImg != null)
                    {
                        string uploadedPath = UploadFile("ProductThumbnail", productVM.ThumbnailImg);
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

                            string uploadedPath = UploadFile("ProductImages", imgFile);

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
                List<pro01product> res = await _dbcontext.pro01product.Where(x => !x.pro01deleted).ToListAsync();
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
                        Area = item.pro01area,
                        Status = item.pro01status,
                        Deleted = item.pro01deleted
                    };
                    List<pro02product_files> images = _dbcontext.pro02product_files.Where(x => x.pro02pro01uin == item.pro01uin).ToList();

                    if (images.Count != 0)
                    {
                        foreach (var image in images)
                        {
                            ProductFiles img = new ProductFiles()
                            {
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
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }


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
                    Area = product.pro01area,
                    Status = product.pro01status,
                    Deleted = product.pro01deleted
                };

                List<pro02product_files> images = _dbcontext.pro02product_files.Where(x => x.pro02pro01uin == product.pro01uin).ToList();

                if (images.Count != 0)
                {
                    foreach (var image in images)
                    {
                        ProductFiles img = new ProductFiles()
                        {
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
        public async Task<ActionResult<AfterSavedResponseVM>> UpdateProductDes([FromForm]ProductResponseVM res)
        {
            try
            {
                pro01product proDetails = _dbcontext.pro01product.Where(x => x.pro01uin == res.ID).FirstOrDefault();
                if (proDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                proDetails.pro01status = res.Status;
                proDetails.pro01code = res.ProductCode;
                proDetails.pro01name = res.ProductName;
                proDetails.pro01cat01uin = res.CategoryId;
                proDetails.pro01cat02uin = res.SubCategoryId;
                proDetails.pro01price = res.Price;
                proDetails.pro01area = res.Area;
                proDetails.pro01map_link = res.MapLink;
                proDetails.pro01video_link = res.VideoLink;
                proDetails.pro01address = res.Address;
                proDetails.pro01property_stats = (EnumPropertyStatus)res.PropertyStatus;
                proDetails.pro01description = res.Description;
                proDetails.pro01details = res.Details;
                proDetails.pro01room_count = res.RoomCount;
                proDetails.pro01bathroom_count = res.BathRoomCount;
                proDetails.pro01deleted = res.Deleted;
                proDetails.pro01updated_date = DateTime.Now;
                proDetails.pro01updated_name = "admin";

                if (res.NewThumbnailImg !=null)
                {
                    if (proDetails.pro01thumbnail_img_path!=null)
                    {
                        DeleteThumbnailImg(proDetails.pro01thumbnail_img_path);
                    }
                    string newFilePath = UploadFile("ProductThumbnail",res.NewThumbnailImg);
                    proDetails.pro01thumbnail_img_path = newFilePath;

                }
                else
                {
                    proDetails.pro01thumbnail_img_path = res.ThumbnailImgPath == null ? "" : res.ThumbnailImgPath;

                }
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


        private string UploadFile(string folderName, IFormFile file)
        {
            try
            {
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                string fileExtension = Path.GetExtension(file!.FileName);

                if (Array.IndexOf(allowedExtensions, fileExtension.ToLower()) == -1)
                {
                    throw new Exception("Invalid file! Only JPG, JPEG, and PNG files are allowed.");
                }

                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file!.FileName)}";
                string yearMonthFolder = DateTime.Now.ToString("yyyy/MM");
                string uploadsFolder = Path.Combine(folderName, yearMonthFolder);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file!.CopyToAsync(stream);
                }

                // Store file information
                var uploadedImage = ($"~/{folderName}/{yearMonthFolder}/{uniqueFileName}");
                return uploadedImage;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
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