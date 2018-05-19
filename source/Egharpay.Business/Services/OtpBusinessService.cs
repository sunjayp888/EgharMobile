using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Enum;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Business.Helpers;
using Egharpay.Business.Extensions;

namespace Egharpay.Business.Services
{
    public class OtpBusinessService : IOtpBusinessService
    {
        private readonly IOtpDataService _otpDataService;
        private readonly ISmsBusinessService _smsBusinessService;
        public OtpBusinessService(IOtpDataService otpDataService, ISmsBusinessService smsBusinessService)
        {
            _otpDataService = otpDataService;
            _smsBusinessService = smsBusinessService;
        }

        public async Task<ValidationResult<AspNetUserMobileOtp>> RetrieveOtp(decimal mobileNumber, int reasonId)
        {
            var validationResult = new ValidationResult<AspNetUserMobileOtp>();
            try
            {
                var data = await _otpDataService.RetrieveAsync<AspNetUserMobileOtp>(e => e.MobileNumber == mobileNumber && e.OTPReasonId == reasonId);
                if (data.FirstOrDefault() != null)
                {
                    validationResult.Entity = data.FirstOrDefault();
                    validationResult.Message = "Valid OTP";
                    validationResult.Succeeded = true;
                }
                else
                {
                    validationResult.Message = "InValid OTP";
                    validationResult.Succeeded = false;
                }
            }
            catch (Exception ex)
            {
                validationResult.Message = ex.Message;
                validationResult.Succeeded = false;
            }
            return validationResult;
        }

        public async Task<ValidationResult<AspNetUserMobileOtp>> CreateLoginOtp(decimal mobileNumber, string ipAddress)
        {
            var validationResult = await CreateOtp(mobileNumber, ipAddress, (int)OtpReason.Login);
            if (validationResult.Succeeded)
            {
                var message = $"Your OTP for login : {validationResult.Entity.OTP} .Do not share your OTP.";
                _smsBusinessService.SendSMS(mobileNumber.ToString(), message);
            }
            return validationResult;
        }

        public async Task<ValidationResult<AspNetUserMobileOtp>> CreateMobileRepairOtp(decimal mobileNumber, string ipAddress)
        {
            var validationResult = await CreateOtp(mobileNumber, ipAddress, (int)OtpReason.MobileRepair);
            if (validationResult.Succeeded)
            {
                var message = $"Your OTP for mobile repair : {validationResult.Entity.OTP}.Do not share your OTP.";
                _smsBusinessService.SendSMS(mobileNumber.ToString(), message);
            }
            return validationResult;
        }

        public async Task<ValidationResult<AspNetUserMobileOtp>> CreateMobileRepairPaymentOtp(decimal mobileNumber, string ipAddress, decimal amount)
        {
            var validationResult = await CreateOtp(mobileNumber, ipAddress, (int)OtpReason.MobileRepairPayment);
            if (validationResult.Succeeded)
            {
                var message = $"Your OTP for mobile repair payment : {validationResult.Entity.OTP} .Please pay amount of {amount}.Do not share your OTP.";
                _smsBusinessService.SendSMS(mobileNumber.ToString(), message);
            }
            return validationResult;
        }

        public async Task<ValidationResult<AspNetUserMobileOtp>> CreateForgetPasswordOtp(decimal mobileNumber, string ipAddress)
        {
            var validationResult = await CreateOtp(mobileNumber, ipAddress, (int)OtpReason.ForgetPassword);
            if (validationResult.Succeeded)
            {
                var message = $"Your OTP to reset password : {validationResult.Entity.OTP}.";
                _smsBusinessService.SendSMS(mobileNumber.ToString(), message);
            }
            return validationResult;
        }

        private async Task<ValidationResult<AspNetUserMobileOtp>> CreateOtp(decimal mobileNumber, string ipAddress, int otpReasonId, string aspNetUserId = null, string message = null)
        {
            var validationResult = new ValidationResult<AspNetUserMobileOtp>();
            if (!mobileNumber.ToString().IsValidMobileNumber())
            {
                validationResult.Message = "Enter valid mobile number.";
                validationResult.Succeeded = false;
                return validationResult;
            }

            var aspnetOtp = new AspNetUserMobileOtp()
            {
                IPAddress = ipAddress,
                MobileNumber = mobileNumber,
                OTP = GenerateOtpHelper.GenerateOtp(),
                OTPCreatedDateTime = DateTime.UtcNow,
                OTPValidDateTime = DateTime.UtcNow.AddMinutes(30),
                OTPReasonId = otpReasonId,
                UserId = aspNetUserId,
            };
            try
            {
                var otpResult = await RetrieveOtp(mobileNumber, otpReasonId);
                if (otpResult.Entity != null)
                    await _otpDataService.DeleteAsync<AspNetUserMobileOtp>(otpResult.Entity);
                var otpGenerated = await _otpDataService.CreateGetAsync(aspnetOtp);
                validationResult.Succeeded = true;
                validationResult.Entity = otpGenerated;
                validationResult.Message = "OTP send successfully";
            }
            catch (Exception ex)
            {
                validationResult.Message = ex.Message;
                validationResult.Succeeded = false;
            }
            return validationResult;
        }

        public async Task<ValidationResult<AspNetUserMobileOtp>> IsValidOtp(int otpNumber, decimal mobileNumber, int reasonId, DateTime? validityDateTime)
        {
            var validationResult = new ValidationResult<AspNetUserMobileOtp>();
            try
            {
                var data = await _otpDataService.RetrieveAsync<AspNetUserMobileOtp>(e => e.MobileNumber == mobileNumber && e.OTPReasonId == reasonId);
                if (validityDateTime.HasValue)
                    data = data.Where(e => e.OTPValidDateTime >= validityDateTime.Value);
                var result = data.FirstOrDefault();
                if (result != null && result.OTP == otpNumber)
                {
                    validationResult.Entity = result;
                    validationResult.Message = "Valid OTP";
                    validationResult.Succeeded = true;
                }
                else
                {
                    validationResult.Message = "InValid OTP";
                    validationResult.Succeeded = false;
                }
            }
            catch (Exception ex)
            {
                validationResult.Message = ex.Message;
                validationResult.Succeeded = false;
            }
            return validationResult;
        }
    }
}
