using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Egharpay.Business.EmailServiceReference;
using Egharpay.Business.Enum;
using Egharpay.Business.Extensions;
using Egharpay.Business.Helpers;
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
        private readonly ISmsBusinessService _smsBusinessService;
        private readonly IMobileRepairMobileFaultBusinessService _mobileRepairMobileFaultBusinessService;
        private readonly IMobileRepairDataService _mobileRepairDataService;
        private readonly IEmailBusinessService _emailBusinessService;
        private readonly ITemplateBusinessService _templateBusinessService;
        private const string MobileRepairRequestEmailTemplate = "MobileRepairRequestCreated";

        public MobileRepairBusinessService(IMobileDataService mobileDataService, ISmsBusinessService smsBusinessService, IMobileRepairMobileFaultBusinessService mobileRepairMobileFaultBusinessService, IMobileRepairDataService mobileRepairDataService, IEmailBusinessService emailBusinessService, ITemplateBusinessService templateBusinessService)
        {
            _mobileDataService = mobileDataService;
            _smsBusinessService = smsBusinessService;
            _mobileRepairMobileFaultBusinessService = mobileRepairMobileFaultBusinessService;
            _mobileRepairDataService = mobileRepairDataService;
            _emailBusinessService = emailBusinessService;
            _templateBusinessService = templateBusinessService;
        }

        #region Create
        public async Task<ValidationResult> Create(MobileRepair mobileRepair)
        {
            var validationResult = new ValidationResult();
            try
            {
                var alreadyCreated = await MobileRepairRequestAlreadyExists(mobileRepair.MobileNumber);
                if (!alreadyCreated.Succeeded)
                    return alreadyCreated;
                mobileRepair.MobileRepairStateId = (int)MobileRepairRequestState.Created;
                var data = await _mobileDataService.CreateGetAsync(mobileRepair);
                await CreateMobileCoupon(mobileRepair.MobileNumber, mobileRepair.CouponCode);

                var message = $"Your request to repair is created.Request id :{data.MobileRepairId}. mumbile team will contact you on {data.MobileNumber}. Thank you for choosing mumbile.com";
                _smsBusinessService.SendSMS(mobileRepair.MobileNumber.ToString(), message);

                //Send Email to Admin
                await SendMobileRepairRequestEmailNotification(data);
                validationResult.Message = "Request created successfully.Mumbile team will call you for appointment and details.";
                validationResult.Succeeded = true;

            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Message = ex.Message;
            }
            return validationResult;
        }

        private async Task CreateMobileRepairMobileFault(int mobileRepairId, List<int> mobileFaultIds)
        {
            var mobileRepairMobileFaultsData = await _mobileRepairMobileFaultBusinessService.RetrieveMobileRepairMobileFaults(mobileRepairId);
            var mobileRepairMobileFaults = mobileRepairMobileFaultsData.ToList();
            var mobileRepairMobileFaultList = new List<MobileRepairMobileFault>();
            foreach (var mobileFaultId in mobileFaultIds)
            {
                if (!mobileRepairMobileFaults.Any(e => e.MobileFaultId == mobileFaultId && e.MobileRepairId == mobileRepairId))
                {
                    mobileRepairMobileFaultList.Add(new MobileRepairMobileFault()
                    {
                        MobileRepairId = mobileRepairId,
                        MobileFaultId = mobileFaultId
                    });
                }
            }
            if (mobileRepairMobileFaultList.Any())
                await _mobileDataService.CreateRangeAsync(mobileRepairMobileFaultList);
        }

        public async Task<ValidationResult> CreateMobileRepairPayment(MobileRepairPayment mobileRepairPayment)
        {
            var validationResult = new ValidationResult();
            try
            {
                await _mobileDataService.CreateAsync(mobileRepairPayment);
                var mobileRepairData = await _mobileDataService.RetrieveByIdAsync<MobileRepair>(mobileRepairPayment.MobileRepairId);
                await UpdateMobileRepairState(mobileRepairPayment.MobileRepairId, (int)MobileRepairRequestState.Completed);
                //Send SMS for mobile repair Payment.
                var message = $"Payment of amount {mobileRepairPayment.Amount} recieved successfully.Invoice will be generated very soon.Thank you for using mumbile.com";
                _smsBusinessService.SendSMS(mobileRepairData.MobileNumber.ToString(), message);
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
        #endregion

        #region Retrieve
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
            var mobileRepair = await _mobileDataService.RetrieveAsync<MobileRepair>(a => a.MobileRepairId == mobileRepairId, null, a => a.MobileRepairMobileFaults);
            return mobileRepair.FirstOrDefault();
        }

        public async Task<PagedResult<MobileRepairGrid>> RetrieveMobileRepairGrids(Expression<Func<MobileRepairGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var mobileRepairGrids = await _mobileDataService.RetrievePagedResultAsync<MobileRepairGrid>(predicate, orderBy, paging);
            return mobileRepairGrids;
        }

        public async Task<PagedResult<MobileFault>> RetrieveMobileFaults(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var mobileFaults = await _mobileRepairDataService.RetrievePagedResultAsync<MobileFault>(a => true, orderBy, paging);
            return mobileFaults;
        }

        #endregion

        #region Update
        public async Task<ValidationResult> UpdateMobileRepairState(int mobileRepairId, int mobileRepairStateId)
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

        public async Task<ValidationResult<MobileRepair>> UpdateMobileRepair(MobileRepair mobileRepair, List<int> mobileFaultIds)
        {
            var validationResult = new ValidationResult<MobileRepair>();
            try
            {
                mobileRepair.AppointmentDate = mobileRepair.AppointmentDate.CombineDateTime(mobileRepair.AppointmentTime);
                await _mobileDataService.UpdateAsync(mobileRepair);
                await CreateMobileRepairMobileFault(mobileRepair.MobileRepairId, mobileFaultIds);
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
        #endregion

        #region Helper
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

        private async Task<ValidationResult> SendMobileRepairRequestEmailNotification(MobileRepair mobileRepair)
        {
            var baseUrl = ConfigHelper.MumbileHost;
            var validationResult = new ValidationResult();
            var mobileRequestEmail = new MobileRepairRequestEmail
            {
                Url = $"{baseUrl}/MobileRepair/Edit/{mobileRepair.MobileRepairId}"
            };
            try
            {
                var templateJson = mobileRequestEmail.ToJson();
                var body = _templateBusinessService.CreateText(templateJson, MobileRepairRequestEmailTemplate);
                if (body != null)
                {
                    await _emailBusinessService.SendEmail(new EmailData
                    {
                        Subject = $"Mobile repair request created by {mobileRepair.MobileNumber}", //ToDo
                        ToAddressList = new List<string>() { ConfigHelper.MobileRepairInfoEmail },
                        IsHtml = true,
                        Body = body,
                    });
                }
                validationResult.Succeeded = true;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }
        #endregion
    }
}
