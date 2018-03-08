using System;

namespace Egharpay.Business.Helpers
{
    public static class GenerateOtpHelper
    {
        public static int GenerateOtp()
        {
            const string numbers = "0123456789";
            var objrandom = new Random();
            var strrandom = string.Empty;
            while (strrandom.Length != 6)
            {
                var temp = objrandom.Next(0, numbers.Length);
                strrandom += temp;
            }
            return Convert.ToInt32(strrandom);
        }
    }
}