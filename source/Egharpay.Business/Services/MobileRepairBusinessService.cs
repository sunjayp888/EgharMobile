using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Egharpay.Business.Enum;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

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
                var alreadyCreated = await MobileRepairRequestAlreadyExists(mobileRepair.MobileNumber);
                if (!alreadyCreated.Succeeded)
                    return alreadyCreated;
                mobileRepair.MobileRepairStateId = (int)MobileRepairRequestState.Created;
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

        public async Task<ValidationResult> CreateMobileRepairPayment(MobileRepairPayment mobileRepair)
        {
            var validationResult = new ValidationResult();
            try
            {
                await _mobileDataService.CreateAsync(mobileRepair);
                await UpdateMobileRepair(mobileRepair.MobileRepairId, (int)MobileRepairRequestState.Completed);
                validationResult.Succeeded = false;
                //Send SMS for mobile repair Payment.
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

        private async Task<ValidationResult> MobileRepairRequestAlreadyExists(decimal mobileNumber)
        {
            var validationResult = new ValidationResult();
            var data = await _mobileDataService.RetrieveAsync<MobileRepair>(m => m.MobileNumber == mobileNumber
                && (m.MobileRepairStateId == (int)MobileRepairRequestState.InProgress ||
                    m.MobileRepairStateId == (int)MobileRepairRequestState.Created));
            if (data.Any())
            {
                validationResult.Message = "Request already created or inprogress.";
                validationResult.Succeeded = false;
            }
            else
            {
                validationResult.Succeeded = true;
            }
            return validationResult;
        }

        public async Task<IEnumerable<MobileRepair>> RetrieveMobileRepair(Expression<Func<MobileRepair, bool>> predicate)
        {
            return await _mobileDataService.RetrieveAsync<MobileRepair>(predicate);
        }

        public async Task<PagedResult<MobileRepair>> RetrieveMobileRepairs(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var mobileRepairs = await _mobileDataService.RetrievePagedResultAsync<MobileRepair>(a => true, orderBy, paging);
            return mobileRepairs;
        }

        public async Task<MobileRepair> RetrieveMobileRepair(int mobileRepairId)
        {
            var mobileRepair = await _mobileDataService.RetrieveAsync<MobileRepair>(a => a.MobileRepairId == mobileRepairId);
            return mobileRepair.FirstOrDefault();
        }

        public async Task<ValidationResult> UpdateMobileRepair(int mobileRepairId, int mobileRepairStateId)
        {
            var validationResult = new ValidationResult();
            try
            {
                var mobileRepair = await _mobileDataService.RetrieveByIdAsync<MobileRepair>(mobileRepairId);
                mobileRepair.MobileRepairStateId = mobileRepairStateId;
                await _mobileDataService.UpdateAsync(mobileRepair);
                validationResult.Succeeded = true;
            }
            catch (Exception e)
            {
                validationResult.Succeeded = true;
                validationResult.Message = e.Message;
            }
            return validationResult;
        }

        public async Task<ValidationResult<MobileRepair>> UpdateMobileRepair(MobileRepair mobileRepair)
        {
            var validationResult = new ValidationResult<MobileRepair>();
            try
            {
                await _mobileDataService.UpdateAsync(mobileRepair);
                validationResult.Succeeded = true;
            }
            catch (Exception e)
            {
                validationResult.Succeeded = true;
                validationResult.Message = e.Message;
            }
            return validationResult;
        }

        public async Task<ValidationResult> CancelMobileRepairRequest(int mobileRepairId)
        {
            var validationResult = new ValidationResult();
            try
            {
                var mobileRepair = await _mobileDataService.RetrieveByIdAsync<MobileRepair>(mobileRepairId);
                mobileRepair.MobileRepairStateId = (int)MobileRepairRequestState.Cancelled;
                await _mobileDataService.UpdateAsync(mobileRepair);
                validationResult.Succeeded = true;
            }
            catch (Exception e)
            {
                validationResult.Succeeded = true;
                validationResult.Message = e.Message;
            }
            return validationResult;
        }
    }
}
