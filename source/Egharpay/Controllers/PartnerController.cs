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
using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Controllers
{
    [RoutePrefix("PartnerEnquiry")]
    public class PartnerController : BaseController
    {
        private readonly IPartnerBusinessService _partnerBusinessService;

        public PartnerController(IPartnerBusinessService partnerBusinessService, IAuthorizationService authorizationService) : base(authorizationService)
        {
            _partnerBusinessService = partnerBusinessService;
        }

        // GET: PartnerEnquiry
        public ActionResult Index()
        {
            return View();
        }

        [Route("Joinus")]
        public ActionResult Joinus()
        {
            return View();

        }
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create(PartnerEnquiry partnerEnquiry)
        {
           var data = await _partnerBusinessService.CreatePartner(partnerEnquiry);
           return this.JsonNet(data);
        }

        [HttpGet]
        [Route("{partnerId:int}/Edit")]
        public async Task<ActionResult> Edit(int partnerId)
        {
            var partner = await _partnerBusinessService.RetrievePartner(partnerId);
            if (partner == null)
            {
                return HttpNotFound();
            }
            var viewModel = new MobileRepairViewModel()
            {
                PartnerEnquiry = partner
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{partnerId:int}/Edit")]
        public async Task<ActionResult> Edit(int partnerId, MobileRepairViewModel mobileRepairViewModel)
        {
            if (ModelState.IsValid)
            {
                mobileRepairViewModel.PartnerEnquiry.PartnerEnquiryId = partnerId;
                var result = await _partnerBusinessService.UpdatePartner(mobileRepairViewModel.PartnerEnquiry);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Exception);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(mobileRepairViewModel);
        }

        [HttpPost]
        [Route("List")]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            try
            {
                var data = await _partnerBusinessService.RetrievePartners(e => true, orderBy, paging);
                return this.JsonNet(data);
            }
            catch (Exception ex)
            {
                return this.JsonNet(""); ;
            }

        }

    }
}