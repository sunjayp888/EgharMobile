using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Business.Helpers;

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

        public async Task<ValidationResult<AspNetUserMobileOtp>> CreateOtp(decimal mobileNumber, string ipAddress, int otpReasonId, string aspNetUserId = null)
        {
            var validationResult = new ValidationResult<AspNetUserMobileOtp>();
            var aspnetOtp = new AspNetUserMobileOtp()
            {
                IPAddress = ipAddress,
                MobileNumber = mobileNumber,
                OTP = GenerateOtpHelper.GenerateOtp(),
                OTPCreatedDateTime = DateTime.UtcNow,
                OTPValidDateTime = DateTime.UtcNow.AddMinutes(2),
                OTPReasonId = otpReasonId,
                UserId = aspNetUserId,
            };
            try
            {
                var otpResult = await RetrieveOtp(mobileNumber, otpReasonId);
                if (otpResult.Entity != null)
                    await _otpDataService.DeleteAsync<AspNetUserMobileOtp>(otpResult.Entity);
                var otpGenerated = await _otpDataService.CreateGetAsync(aspnetOtp);
                _smsBusinessService.SendSMS(mobileNumber.ToString(), otpGenerated.OTP.ToString());
                //Send to mobile
                validationResult.Succeeded = true;
                validationResult.Message = "OTP send successfully";
            }
            catch (Exception ex)
            {
                validationResult.Message = ex.Message;
                validationResult.Succeeded = false;
            }
            return validationResult;
        }

        public async Task<ValidationResult<AspNetUserMobileOtp>> IsValidOtp(int otpNumber, decimal mobileNumber, int reasonId)
        {
            var validationResult = new ValidationResult<AspNetUserMobileOtp>();
            try
            {
                var data = await _otpDataService.RetrieveAsync<AspNetUserMobileOtp>(e => e.MobileNumber == mobileNumber && e.OTPReasonId == reasonId);
                var result = data.ToList().FirstOrDefault();
                if (result != null && result.OTP == otpNumber)
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
    }
}
