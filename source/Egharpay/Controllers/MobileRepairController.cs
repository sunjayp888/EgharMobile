using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Enum;
using Egharpay.Business.Interfaces;
using Egharpay.Models;

namespace Egharpay.Controllers
{
    public class MobileRepairController : BaseController
    {
        private readonly IOtpBusinessService _otpBusinessService;

        public MobileRepairController(IOtpBusinessService otpBusinessService)
        {
            _otpBusinessService = otpBusinessService;
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
            var otpValidationResult = await _otpBusinessService.IsValidOtp(Convert.ToInt32(model.OTP), Convert.ToDecimal(model.MobileNumber), (int)OtpReason.Login, DateTime.UtcNow);
            return this.Json(otpValidationResult);
        }

    }
}