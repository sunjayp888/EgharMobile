using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Controllers
{
    public class HomeBannerController : BaseController
    {
        private readonly IHomeBannerBusinessService _homeBannerBusinessService;
        private readonly IMobileBusinessService _mobileBusinessService;

        public HomeBannerController(IHomeBannerBusinessService homeBannerBusinessService, IMobileBusinessService mobileBusinessService,IConfigurationManager configurationManager, IAuthorizationService authorizationService) : base(configurationManager, authorizationService)
        {
            _homeBannerBusinessService = homeBannerBusinessService;
            _mobileBusinessService = mobileBusinessService;
        }

        // GET: HomeBanner
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: HomeBanner/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            var mobile = await _mobileBusinessService.RetrieveMobiles();
            var mobiles = mobile.Items.ToList();
            var viewModel = new HomeBannerViewModel()
            {
                HomeBanner = new HomeBanner(),
                Mobiles = new SelectList(mobiles, "MobileId", "Name")
            };
            return View(viewModel);
        }

        // POST: HomeBanner/Create
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HomeBannerViewModel homeBannerViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create HomeBanner
                var result = await _homeBannerBusinessService.CreateHomeBanner(homeBannerViewModel.HomeBanner);
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
            return View(homeBannerViewModel);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var homeBanner = await _homeBannerBusinessService.RetrieveHomeBanner(id.Value);
            var mobile = await _mobileBusinessService.RetrieveMobiles();
            var mobiles = mobile.Items.ToList();
            if (homeBanner == null)
            {
                return HttpNotFound();
            }
            var viewModel = new HomeBannerViewModel()
            {
                HomeBanner = homeBanner,
                Mobiles = new SelectList(mobiles, "MobileId", "Name")
            };
            return View(viewModel);
        }

        // POST: HomeBanner/Create
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HomeBannerViewModel homeBannerViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create HomeBanner
                var result = await _homeBannerBusinessService.UpdateHomeBanner(homeBannerViewModel.HomeBanner);
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
            return View(homeBannerViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _homeBannerBusinessService.RetrieveHomeBanners(orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> HomeBannerImage(DateTime startDateTime, DateTime endDateTime, string pincode)
        {
            var data = await _homeBannerBusinessService.RetrieveHomeBannerImages(startDateTime, endDateTime, pincode);
            return this.JsonNet(data);
        }
    }
}