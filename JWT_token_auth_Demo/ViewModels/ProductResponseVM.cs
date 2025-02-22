﻿using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.ViewModels
{
    public class ProductResponseVM
    {
        public ProductResponseVM()
        {
            Images = new List<ProductFiles>();
        }
        public string ID { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? CategoryId { get; set; }
        public string? SubCategoryId { get; set; }
        public double? Price { get; set; }
        public string? Address { get; set; }
        public string? MapLink { get; set; }
        public string? ThumbnailImgPath { get; set; }
        public IFormFile? NewThumbnailImg { get; set; }
        public string? VideoLink { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public string? PropertyStatusValue { get; set; }
        public int? PropertyStatus { get; set; }
        public int? RoomCount { get; set; }
        public int? BathRoomCount { get; set; }
        public int? ParkingCount { get; set; }
        public double? Area { get; set; }
        public bool? Status { get; set; }
        public bool? Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<ProductFiles>? Images { get; set; }
        public List<IFormFile>? NewImgFile { get; set; }

    }

    public class ProductFiles
    {
        public string? ImageId { get; set; }
        public string? Name { get; set; }
        public string? FilePath { get; set; }
        public DateTime? UploadedDate { get; set; }
    }
}
