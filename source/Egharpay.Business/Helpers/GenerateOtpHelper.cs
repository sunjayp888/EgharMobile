using System;

namespace Egharpay.Business.Helpers
{
    public static class GenerateOtpHelper
    {
        public static int GenerateOtp()
        {
            const int length = 6;
            const string numbers = "0123456789";
            var objrandom = new Random();
            var strrandom = string.Empty;
            const int noofnumbers = length;
            for (var i = 0; i < noofnumbers; i++)
            {
                var temp = objrandom.Next(0, numbers.Length);
                strrandom += temp;
            }
            return Convert.ToInt32(strrandom);
        }
    }
}