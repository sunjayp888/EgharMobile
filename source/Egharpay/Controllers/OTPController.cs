using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Egharpay.Business.Enum;
using Egharpay.Business.Interfaces;
using Egharpay.Extensions;

namespace Egharpay.Controllers
{
    public class OTPController : BaseController
    {
        private readonly IOtpBusinessService _otpBusinessService;
        private readonly IPersonnelBusinessService _personnelBusinessService;

        public OTPController(IOtpBusinessService otpBusinessService, IPersonnelBusinessService personnelBusinessService) : base()
        {
            _otpBusinessService = otpBusinessService;
            _personnelBusinessService = personnelBusinessService;
        }

        // GET: OTP
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("OTP/CreateLoginOtp")]
        public async Task<ActionResult> CreateLoginOtp(string mobileNumber)
        {
            var ipAddress = Request.UserHostAddress;
            var personnelResult = await _personnelBusinessService.PersonnelAlreadyExists(mobileNumber);
            if (!personnelResult.Succeeded)
                return this.JsonNet(personnelResult);
            var data = await _otpBusinessService.CreateLoginOtp(Convert.ToDecimal(mobileNumber), ipAddress);
            return this.JsonNet(data);
        }

        [HttpPost]
        [Route("OTP/CreateMobileRepairOtp")]
        public async Task<ActionResult> CreateMobileRepairOtp(decimal mobileNumber)
        {
            var ipAddress = Request.UserHostAddress;
            var data = await _otpBusinessService.CreateMobileRepairOtp(Convert.ToDecimal(mobileNumber), ipAddress);
            return this.JsonNet(data);
        }

        [HttpPost]
        [Route("OTP/RetrieveLoginOtp")]
        public async Task<ActionResult> RetrieveLoginOtp(decimal mobileNumber)
        {
            var data = await _otpBusinessService.RetrieveOtp(mobileNumber, (int)OtpReason.Login);
            return this.JsonNet(data);
        }

        [HttpPost]
        [Route("OTP/CreateMobileRepairPaymentOtp")]
        public async Task<ActionResult> CreateMobileRepairPaymentOtp(decimal mobileNumber)
        {
            var ipAddress = Request.UserHostAddress;
            var data = await _otpBusinessService.CreateMobileRepairPaymentOtp(Convert.ToDecimal(mobileNumber), ipAddress);
            return this.JsonNet(data);
        }
    }
}