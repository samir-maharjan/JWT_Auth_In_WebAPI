﻿using System.ComponentModel.DataAnnotations;

namespace JWT_token_auth_Demo.ViewModels
{
    public class ProductVM
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public double Price { get; set; }
        public string Address { get; set; }
        public string MapLink { get; set; }
        public string VideoLink { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public int RoomCount { get; set; }
        public int BathRoomCount { get; set; }
        public int ParkingCount { get; set; }
        public int PropertyStatus { get; set; }
        public IFormFile? ThumbnailImg { get; set; }
        public double Area { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public ProductFileVM ProductFiles { get; set; }

    }

    public class ProductSearchVM
    {
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public int? RoomCount { get; set; }
        public int? BathRoomCount { get; set; }
        public int? ParkingCount { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public double? MinArea { get; set; }
        public double? MaxArea { get; set; }
    }

    public class ProductFileVM
    {
        public List<IFormFile>? ImgFile { get; set; }

    }
}
