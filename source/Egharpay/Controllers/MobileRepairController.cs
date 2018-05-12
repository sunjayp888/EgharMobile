using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Egharpay.Business.Enum;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Egharpay.Models.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Authorization;
using Role = Egharpay.Enums.Role;

namespace Egharpay.Controllers
{

    public class MobileRepairController : BaseController
    {
        private readonly IMobileRepairBusinessService _mobileRepairBusinessService;
        private readonly ICouponCodeBusinessService _couponCodeBusinessService;
        private readonly IOtpBusinessService _otpBusinessService;
        private readonly IMobileRepairAdminBusinessService _mobileRepairAdminBusinessService;

        public MobileRepairController(IMobileRepairBusinessService mobileRepairBusinessService, IOtpBusinessService otpBusinessService, ICouponCodeBusinessService couponCodeBusinessService,
            IMobileRepairAdminBusinessService mobileRepairAdminBusinessService, IAuthorizationService authorizationService) : base(authorizationService)
        {
            _mobileRepairBusinessService = mobileRepairBusinessService;
            _otpBusinessService = otpBusinessService;
            _couponCodeBusinessService = couponCodeBusinessService;
            _mobileRepairAdminBusinessService = mobileRepairAdminBusinessService;
        }

        // GET: MobileRepair
        public ActionResult Index()
        {
            return View();
        }

        // GET: MobileRepair
        public async Task<ActionResult> MobileRepairOrder()
        {
            var id = UserPersonnelId;
            if (id == 0)
                return HttpForbidden();
            if (!await AuthorizationService.AuthorizeAsync((ClaimsPrincipal)User, id, Policies.Resource.MobileRepair.ToString()))
                return HttpForbidden();
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
            var mobileRepairResult = await _mobileRepairBusinessService.RetrieveMobileRepairGrids(e => e.MobileNumber == mobileNumber);
            return this.JsonNet(mobileRepairResult.Items);
        }

        [HttpPost]
        [Route("MobileRepair/List")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            try
            {
                if (User.IsMobileRepairAdmin())
                {
                    var userId = UserPersonnelId;
                    return this.JsonNet(await _mobileRepairBusinessService.RetrieveMobileRepairGrids(e => e.MobileRepairAdminPersonnelId == userId, orderBy, paging));
                }
                return this.JsonNet(await _mobileRepairBusinessService.RetrieveMobileRepairGrids(e => true, orderBy, paging));
            }
            catch (Exception)
            {
                return this.JsonNet("");
            }
        }

        [HttpPost]
        [Route("MobileRepair/MarkAsCompleted")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
        public async Task<ActionResult> MarkAsCompleted(int mobileRepairId)
        {
            var id = UserPersonnelId;
            if (id == 0)
                return HttpForbidden();
            if (!await AuthorizationService.AuthorizeAsync((ClaimsPrincipal)User, id, Policies.Resource.MobileRepair.ToString()))
                return HttpForbidden();
            var result = await _mobileRepairBusinessService.UpdateMobileRepairState(mobileRepairId, (int)MobileRepairRequestState.Completed);
            return this.JsonNet(result);
        }

        [HttpPost]
        [Route("MobileRepair/MarkAsCancelled")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
        public async Task<ActionResult> MarkAsCancelled(int mobileRepairId)
        {
            var result = await _mobileRepairBusinessService.UpdateMobileRepairState(mobileRepairId, (int)MobileRepairRequestState.Cancelled);
            return this.JsonNet(result);
        }

        [HttpPost]
        [Route("MobileRepair/MarkAsInProgress")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
        public async Task<ActionResult> MarkAsInProgress(int mobileRepairId)
        {
            var result = await _mobileRepairBusinessService.UpdateMobileRepairState(mobileRepairId, (int)MobileRepairRequestState.InProgress);
            return this.JsonNet(result);
        }

        [HttpPost]
        [Route("MobileRepair/UpdateMobileRepairState")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
        public async Task<ActionResult> UpdateMobileRepairState(int mobileRepairId, int mobileRepairStateId)
        {
            var id = UserPersonnelId;
            if (id == 0)
                return HttpForbidden();
            if (!await AuthorizationService.AuthorizeAsync((ClaimsPrincipal)User, id, Policies.Resource.MobileRepair.ToString()))
                return HttpForbidden();
            var result = await _mobileRepairBusinessService.UpdateMobileRepairState(mobileRepairId, mobileRepairStateId);
            return this.JsonNet(result);
        }

        [HttpPost]
        [Route("MobileRepair/DeleteMobileRepairRequest")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
        public async Task<ActionResult> DeleteMobileRepairRequest(int mobileRepairId, decimal mobileNumber, int otp)
        {
            var otpResult = await _otpBusinessService.IsValidOtp(otp, mobileNumber, (int)OtpReason.MobileRepair, DateTime.UtcNow);
            if (!otpResult.Succeeded)
                return this.JsonNet(otpResult);
            return this.JsonNet(await _mobileRepairBusinessService.CancelMobileRepairRequest(mobileRepairId));
        }

        [HttpPost]
        [Route("MobileRepair/CreateMobileRepairPayment")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
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

        [Route("MobileRepair/Edit/{mobileRepairId}")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
        public async Task<ActionResult> Edit(int mobileRepairId)
        {
            var id = UserPersonnelId;
            if (id == 0)
                return HttpForbidden();
            if (!await AuthorizationService.AuthorizeAsync((ClaimsPrincipal)User, id, Policies.Resource.MobileRepair.ToString()))
                return HttpForbidden();
            var mobileRepair = await _mobileRepairBusinessService.RetrieveMobileRepair(mobileRepairId);
            var model = new MobileRepairViewModel()
            {
                MobileRepair = mobileRepair
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("MobileRepair/Edit/{mobileRepairId}")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
        public async Task<ActionResult> Edit(MobileRepairViewModel model)
        {
            await _mobileRepairBusinessService.UpdateMobileRepair(model.MobileRepair);
            return RedirectToAction("MobileRepairOrder");
        }

        [HttpPost]
        [Route("MobileRepair/RetrieveAvailableMobileRepairAdmin")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
        public async Task<ActionResult> RetrieveAvailableMobileRepairAdmin(DateTime? date, string time)
        {
            var fromDateTime = date.CombineDateTime(time);
            var data = await _mobileRepairAdminBusinessService.RetrieveAvailableMobileRepairAdmin(date, time);
            return this.JsonNet(data);
        }


        [Route("MobileRepair/RetrieveMobileRepairAdmins")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
        public async Task<ActionResult> RetrieveMobileRepairAdmins()
        {
            var data = await _mobileRepairAdminBusinessService.RetrieveAvailableMobileRepairAdmins();
            return this.JsonNet(data);
        }

        [HttpPost]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(_mobileRepairBusinessService.RetrieveMobileRepairGrids(e => e.AppointmentDate >= fromDate && e.AppointmentDate <= toDate, orderBy, paging));
        }

    }
}