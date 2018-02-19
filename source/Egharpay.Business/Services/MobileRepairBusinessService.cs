using System;
using System.Linq;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;

namespace Egharpay.Business.Services
{
    public class MobileRepairBusinessService : IMobileRepairBusinessService
    {
        private readonly IMobileDataService _mobileDataService;

        public MobileRepairBusinessService(IMobileDataService mobileDataService)
        {
            _mobileDataService = mobileDataService;
        }

        public async Task<ValidationResult> Create(MobileRepair mobileRepair)
        {
            var validationResult = new ValidationResult();
            try
            {
                await _mobileDataService.CreateAsync(mobileRepair);
                await CreateMobileCoupon(mobileRepair.MobileNumber, mobileRepair.CouponCode);
                validationResult.Message = "Request created successfully.";
                validationResult.Succeeded = true;

            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Message = ex.Message;
            }
            return validationResult;
        }

        private async Task CreateMobileCoupon(decimal mobileNumber, string couponCode)
        {
            var couponResult = await _mobileDataService.RetrieveAsync<CouponCode>(e => e.Code.ToLower() == couponCode.ToLower());
            var couponId = couponResult.FirstOrDefault()?.CouponCodeId;
            if (couponId.HasValue)
            {
                var mobileCoupon = new MobileCoupon()
                {
                    MobileNumber = mobileNumber,
                    CouponId = couponId.Value,
                    UsedDate = DateTime.UtcNow
                };
                await _mobileDataService.CreateAsync(mobileCoupon);
            }
        }
    }
}
