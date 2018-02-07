using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Egharpay.Extensions
{
    public static class StringExtensions
    {
        public static int? ToNullableInt(this string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }

        public static string ArrayToString<T>(this T[] obj)
        {
            return string.Join(",", obj.Select(o => o.ToString()));
        }

        public static T[] StringToArray<T>(this string @string)
        {
            return (from piece in @string.Split(',')
                    let trimmed = piece.Trim()
                    where !string.IsNullOrEmpty(trimmed)
                    select trimmed.ToEnum<T>()).ToArray();
        }

        public static string AsString(this IEnumerable<string> source, string separator = "")
        {
            return string.Join(separator, source);
        }

        public static byte[] ToByteArray(this string @string)
        {
            return Encoding.UTF8.GetBytes(@string);
        }

        public static bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }

        public static bool IsValidMobileNumber(this string @mobileNumber)
        {
            return IsNumeric(@mobileNumber) && @mobileNumber.Length == 0;
        }

        public static bool IsValidEmail(this string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        public static bool IsValidUserName(this string userName)
        {
            return new EmailAddressAttribute().IsValid(userName) || (IsNumeric(userName) && userName.Length == 10);
        }
    }
}