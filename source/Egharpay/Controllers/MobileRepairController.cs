using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Enum;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Microsoft.AspNet.Identity;

namespace Egharpay.Controllers
{
    public class MobileRepairController : BaseController
    {
        private readonly IMobileRepairBusinessService _mobileRepairBusinessService;
        private readonly ICouponCodeBusinessService _couponCodeBusinessService;
        private readonly IOtpBusinessService _otpBusinessService;

        public MobileRepairController(IMobileRepairBusinessService mobileRepairBusinessService, IOtpBusinessService otpBusinessService, ICouponCodeBusinessService couponCodeBusinessService)
        {
            _mobileRepairBusinessService = mobileRepairBusinessService;
            _otpBusinessService = otpBusinessService;
            _couponCodeBusinessService = couponCodeBusinessService;
        }

        // GET: MobileRepair
        public ActionResult Index()
        {
            return View();
        }

        // GET: MobileRepair
        public ActionResult MobileRepairOrder()
        {
            return View(new MobileRepairViewModel());
        }

        [HttpPost]
        [Route("MobileRepair/Create")]
        public async Task<ActionResult> Create(MobileRepairViewModel model)
        {
            var mobileRepair = new MobileRepair()
            {
                CouponCode = model.CouponCode,
                Description = model.Description,
                MobileNumber = model.MobileNumber,
                ModelName = model.ModelName
            };
            var otpResult = await _otpBusinessService.IsValidOtp(model.OTP, model.MobileNumber, (int)OtpReason.MobileRepair, DateTime.UtcNow);
            if (!otpResult.Succeeded)
                return this.Json(otpResult);
            if (!string.IsNullOrEmpty(model.CouponCode))
            {
                var couponCodeResult = await _couponCodeBusinessService.IsValidCoupon(model.MobileNumber, model.CouponCode);
                if (!couponCodeResult.Succeeded)
                    return this.Json(couponCodeResult);
            }
            var mobileRepairResult = await _mobileRepairBusinessService.Create(mobileRepair);
            return this.JsonNet(mobileRepairResult);
        }

        [HttpGet]
        [Route("MobileRepair/RetrieveMobileRepairOrdersByMobile/{mobileNumber}/{otp}")]
        public async Task<ActionResult> RetrieveMobileRepairOrdersByMobile(decimal mobileNumber, int otp)
        {
            var otpResult = await _otpBusinessService.IsValidOtp(otp, mobileNumber, (int)OtpReason.MobileRepair, DateTime.UtcNow);
            if (!otpResult.Succeeded)
                return this.JsonNet(otpResult);
            var mobileRepairResult = await _mobileRepairBusinessService.RetrieveMobileRepair(e => e.MobileNumber == mobileNumber);
            return this.JsonNet(mobileRepairResult);
        }

        [HttpPost]
        [Route("MobileRepair/List")]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            try
            {
                var data = await _mobileRepairBusinessService.RetrieveMobileRepairs(orderBy, paging);
                return this.JsonNet(data);
            }
            catch (Exception)
            {
                return this.JsonNet("");
            }
        }

        [HttpPost]
        [Route("MobileRepair/MarkAsCompleted")]
        public async Task<ActionResult> MarkAsCompleted(int mobileRepairId)
        {
            var result = await _mobileRepairBusinessService.UpdateMobileRepair(mobileRepairId, (int)MobileRepairRequestState.Completed);
            return this.JsonNet(result);
        }

        [HttpPost]
        [Route("MobileRepair/MarkAsCancelled")]
        public async Task<ActionResult> MarkAsCancelled(int mobileRepairId)
        {
            var result = await _mobileRepairBusinessService.UpdateMobileRepair(mobileRepairId, (int)MobileRepairRequestState.Cancelled);
            return this.JsonNet(result);
        }

        [HttpPost]
        [Route("MobileRepair/UpdateMobileRepairState")]
        public async Task<ActionResult> UpdateMobileRepairState(int mobileRepairId, int mobileRepairStateId)
        {
            var result = await _mobileRepairBusinessService.UpdateMobileRepair(mobileRepairId, mobileRepairStateId);
            return this.JsonNet(result);
        }

        [HttpPost]
        [Route("MobileRepair/DeleteMobileRepairRequest")]
        public async Task<ActionResult> DeleteMobileRepairRequest(int mobileRepairId, decimal mobileNumber, int otp)
        {
            var otpResult = await _otpBusinessService.IsValidOtp(otp, mobileNumber, (int)OtpReason.MobileRepair, DateTime.UtcNow);
            if (!otpResult.Succeeded)
                return this.JsonNet(otpResult);
            return this.JsonNet(await _mobileRepairBusinessService.CancelMobileRepairRequest(mobileRepairId));
        }

        [HttpPost]
        [Route("MobileRepair/CreateMobileRepairPayment")]
        public async Task<ActionResult> CreateMobileRepairPayment(MobileRepairViewModel model)
        {
            var otpResult = await _otpBusinessService.IsValidOtp(model.OTP, model.MobileNumber, (int)OtpReason.MobileRepairPayment, DateTime.UtcNow);
            if (!otpResult.Succeeded)
                return this.JsonNet(otpResult);
            var mobilePayment = new MobileRepairPayment
            {
                Amount = model.Amount,
                MobileRepairId = model.MobileRepairId,
                Otp = model.OTP,
                RecievedBy = User.Identity.GetUserId()
            };
            var result = await _mobileRepairBusinessService.CreateMobileRepairPayment(mobilePayment);
            return this.JsonNet(result);
        }

    }
}