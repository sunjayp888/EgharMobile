using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Enum;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Models;

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
                var couponCodeResult = await _couponCodeBusinessService.IsValidCoupon(model.MobileNumber, model.CouponCode, TODO);
                if (!couponCodeResult.Succeeded)
                    return this.Json(couponCodeResult);
            }
            var mobileRepairResult = await _mobileRepairBusinessService.Create(mobileRepair);
            return this.Json(mobileRepairResult);
        }
    }
}