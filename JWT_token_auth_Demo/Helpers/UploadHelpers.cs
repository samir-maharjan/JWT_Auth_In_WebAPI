using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using JWT_token_auth_Demo.StaticHelper;
using System;
using System.Linq;
//using System.IO.Path;

namespace OnlineFormAPI.Helpers
{
    public class UploadResponse
    {
        public bool Status { get; set; }
        public string FileName { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string PhysicalPath { get; set; }
        public string RelativePath { get; set; }
        public string oldName { get; set; }
    }
    public static class UploadHelpers
    {
        public static IWebHostEnvironment Environment = null;
        public static bool SetEnv(IWebHostEnvironment Env)
        {
            if (Environment == null)
            {
                Environment = Env;
            }
            return true;
        }
        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return string.Concat(Path.GetFileNameWithoutExtension(fileName)
                                , "_"
                                , Guid.NewGuid().ToString().AsSpan(0, 4)
                                , Path.GetExtension(fileName));
        }


        public static UploadResponse UploadFile(IFormFile MainPhoto, string Destination, GeneralAppConfig AppConfig)
        {
            var allowedExtensions = new[] { ".jpg", ".gif", ".png", ".jpeg", ".pdf" };
            return UploadFile(MainPhoto, Destination, allowedExtensions, "", AppConfig);
        }
        public static UploadResponse UploadFile(IFormFile MainPhoto, string Destination, string OldFileName, GeneralAppConfig AppConfig)
        {
            var allowedExtensions = new[] { ".jpg", ".gif", ".png", ".jpeg" };
            return UploadFile(MainPhoto, Destination, allowedExtensions, OldFileName, AppConfig);
        }
        public static UploadResponse UploadFile(IFormFile MainPhoto, string Destination, string[] allowedExtensions, GeneralAppConfig AppConfig)
        {
            return UploadFile(MainPhoto, Destination, allowedExtensions, "", AppConfig);
        }

        private static string CopyUploadedFile(IFormFile MainPhoto, string RootPath, string DestinationPath, string newName)
        {
            string uploads = RootPath + "/Private/" + DestinationPath.TrimStart('~');
            string filePath = Path.Combine(uploads, newName);
            if (!System.IO.Directory.Exists(uploads))
            {
                _ = System.IO.Directory.CreateDirectory(uploads);
            }
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            //System.IO.File.Copy()
            MainPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
            return filePath;
        }
        private static string CopyLocalFile(string sourceFile, string RootPath, string DestinationPath, string newName)
        {
            string uploads = RootPath + "/Private/" + DestinationPath.TrimStart('~');
            string filePath = Path.Combine(uploads, newName);
            if (!System.IO.Directory.Exists(uploads))
            {
                _ = System.IO.Directory.CreateDirectory(uploads);
            }
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            System.IO.File.Copy(sourceFile, filePath);
            //MainPhoto.CopyTo(new FileStream(filePath, FileMode.Create));
            return filePath;
        }
        public static UploadResponse UploadFile(IFormFile MainPhoto, string Destination, string[] allowedExtensions, string OldFileName, GeneralAppConfig AppConfig)
        {
            var imageExt = new[] { ".jpg", ".gif", ".png", ".jpeg", ".pdf" };
            UploadResponse NewResponse = new UploadResponse() { Status = false, ErrorCode = 999, ErrorMessage = "Unknown" };

            string newName = "";
            string oldName = "";

            if (MainPhoto != null && MainPhoto.Length > 0)
            {
                var newExt = Path.GetExtension(MainPhoto.FileName).ToLower();
                newName = !string.IsNullOrEmpty(OldFileName) ? OldFileName : GetUniqueFileName(MainPhoto.FileName);
                string datePath = DateTime.UtcNow.ToString("/yyyy/MM/dd/");
                string datedDesitnation = Destination + datePath;
                oldName = MainPhoto.FileName;
                if (allowedExtensions.Contains(newExt))
                {
                    try
                    {
                        string filePath = CopyUploadedFile(MainPhoto, Environment.ContentRootPath, datedDesitnation, newName);
                        if (AppConfig.CopyUploadedFiles && !string.IsNullOrEmpty(AppConfig.CopyFileLocations) && AppConfig.CopyFileLocations.Length > 0)
                        {
                            //CopyUploadedFile(MainPhoto, AppConfig.CopyFileLocations, datedDesitnation, newName);
                            CopyLocalFile(filePath, AppConfig.CopyFileLocations, datedDesitnation, newName);
                        }

                        //resize code
                        //resize

                        NewResponse.Status = true;
                        NewResponse.FileName = newName;
                        NewResponse.PhysicalPath = filePath;
                        NewResponse.RelativePath = "~/Private" + datedDesitnation.TrimStart('~') + newName;
                        NewResponse.oldName = oldName;
                    }
                    catch (Exception ex)
                    {
                        NewResponse.ErrorCode = 501;
                        NewResponse.ErrorMessage = ex.Message;
                    }
                }
                else
                {
                    NewResponse.ErrorCode = 102;
                    NewResponse.ErrorMessage = "Invalid File Type";
                }
            }
            else
            {
                NewResponse.ErrorCode = 101;
                NewResponse.ErrorMessage = "No File uploaded";
            }
            return NewResponse;
        }

    }
}
