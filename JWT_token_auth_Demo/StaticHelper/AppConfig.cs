

namespace JWT_token_auth_Demo.StaticHelper
{
    public static class StaticGeneralAppConfig
    {
        public const string AppSettings = "AppSettings";
        public static GeneralAppConfig Config { get; set; } = new GeneralAppConfig();

    }
    public class GeneralAppConfig
    {
        public bool IsUATEnvironment { get; set; }
        public bool HasEmailNotification { get; set; }
        public bool CopyUploadedFiles { get; set; }
        public string CopyFileLocations { get; set; }
        public bool EnableTestController { get; set; }
        public bool LogAPIResult { get; set; } = true;

    }
}
