using System;
using Egharpay.Business.Models;
using System.Linq;

namespace Egharpay.Business.Extensions
{
    public static class ValidationExtensions
    {
        public static ValidationResult Error(this ValidationResult result, Exception exception)
        {
            result.Succeeded = false;
            result.Exception = exception;            
            result.Errors = new[] { exception.InnerMessage() };
            return result;
        }
        public static ValidationResult Error(this ValidationResult result, string message)
        {
            result.Succeeded = false;
            result.Message = message;
            result.Errors = new[] { message };
            return result;
        }
        public static ValidationResult Error(this ValidationResult result, string[] messages)
        {
            result.Succeeded = false;
            result.Message = string.Join(",", messages);
            result.Errors = messages;
            return result;
        }
        public static ValidationResult<T> Error<T>(this ValidationResult<T> result, string message) where T : class
        {
            result.Succeeded = false;
            result.Message = message;
            result.Errors = new[] { message };
            return result;
        }
        public static ValidationResult<T> Error<T>(this ValidationResult<T> result, string[] messages) where T : class
        {
            result.Succeeded = false;
            result.Message = string.Join(",", messages);
            result.Errors = messages;
            return result;
        }
        public static ValidationResult Success(this ValidationResult result, string message)
        {
            result.Succeeded = true;
            result.Message = message;
            return result;
        }

        public static ValidationResult<T> Success<T>(this ValidationResult<T> result, T entity, string message) where T : class
        {
            result.Succeeded = true;
            result.Message = message;
            result.Entity = entity;
            return result;
        }

        public static bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }

        public static bool IsValidMobileNumber(this string mobileNumber)
        {
            return IsNumeric(mobileNumber) && mobileNumber.Length == 10;
        }

    }
}
