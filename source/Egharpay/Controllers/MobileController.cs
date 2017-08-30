using System;
using System.Collections.Generic;
using System.Linq;
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
    public class MobileController : BaseController
    {
        private readonly IMobileBusinessService _mobileBusinessService;
        private readonly IBrandBusinessService _brandBusinessService;
        //public MobileController(IMobileBusinessService mobileBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService, IBrandBusinessService brandBusinessService) : base(configurationManager, authorizationService)
        //{
        //    _mobileBusinessService = mobileBusinessService;
        //    _brandBusinessService = brandBusinessService;
        //}

        // GET: Mobile
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Apartment/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            var brands = await _brandBusinessService.RetrieveBrands();
            var brandList = brands.Items.ToList();
            var viewModel = new MobileViewModel()
            {
                Mobile = new Mobile(),
                Brands = new SelectList(brandList, "BrandId", "Name")
            };
            return View(viewModel);
        }

        // POST: Apartment/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MobileViewModel mobileViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create Apartment
                var result = await _mobileBusinessService.CreateMobile(mobileViewModel.Mobile);
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
            return View(mobileViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _mobileBusinessService.RetrieveMobiles(orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(await _mobileBusinessService.Search(searchKeyword, orderBy, paging));
        }
    }
}