using System;
using System.Configuration;

namespace Egharpay.Business.Helpers
{
    public static class ConfigHelper
    {
        public static string DefaultConnection => ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public static int CacheTimeout => Convert.ToInt32(ConfigurationManager.AppSettings["CacheTimeout"]);
        public static string EmailDefaultFromAddress => ConfigurationManager.AppSettings["EmailDefaultFromAddress"].ToString();
        public static string OverrideEmailAddresses => ConfigurationManager.AppSettings["OverrideEmailAddresses"].ToString();
        public static string TemporaryMobileGalleryImagePath => ConfigurationManager.AppSettings["TemporaryMobileGalleryImagePath"].ToString();
        public static string TemplateRootFilePath => ConfigurationManager.AppSettings["TemplateRootFilePath"].ToString();
        public static bool SendSMS => ConfigurationManager.AppSettings["SendSMS"].ToString() == "true";
        public static string MumbileHost => ConfigurationManager.AppSettings["MumbileHost"];
        public static string MobileRepairInfoEmail => ConfigurationManager.AppSettings["MobileRepairInfoEmail"];
        
    }
}
