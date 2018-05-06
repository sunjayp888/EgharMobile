using System;

namespace Egharpay.Business.Helpers
{
    public static class GenerateOtpHelper
    {
        public static int GenerateOtp()
        {
            string numbers = "1234567890";
            string characters = numbers;
            string otp = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return Convert.ToInt32(otp);
        }
    }
}