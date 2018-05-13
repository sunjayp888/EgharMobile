using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.Enum;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Egharpay.Models.Authorization;
using Microsoft.Owin.Security.Authorization;
using Role = Egharpay.Enums.Role;

namespace Egharpay.Controllers
{
    [RoutePrefix("Seller")]
    [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.Seller })]
    public class SellerController : BaseController
    {
        private readonly ISellerBusinessService _sellerBusinessService;
        public SellerController(ISellerBusinessService sellerBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService) : base(configurationManager, authorizationService)
        {
            _sellerBusinessService = sellerBusinessService;
        }

        // GET: Seller
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [Route("Create")]
        public async Task<ActionResult> Create()
        {
            var viewModel = new SellerViewModel()
            {
                Seller = new Seller()
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SellerViewModel sellerViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create Seller
                sellerViewModel.Seller.CreatedDate = DateTime.UtcNow;
                sellerViewModel.Seller.ApprovalStateId = (int)SellerApprovalState.Pending;
                sellerViewModel.Seller.ApprovalStateId = (int)SellerApprovalState.Pending;
                var result = await _sellerBusinessService.CreateSeller(sellerViewModel.Seller);
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
            return View(sellerViewModel);
        }

        [HttpGet]
        [Route("{sellerId:int}/Edit")]
        public async Task<ActionResult> Edit(int sellerId)
        {
            var seller = await _sellerBusinessService.RetrieveSeller(sellerId);
            if (seller == null)
            {
                return HttpNotFound();
            }
            var viewModel = new SellerViewModel()
            {
                Seller = seller
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{sellerId:int}/Edit")]
        public async Task<ActionResult> Edit(int sellerId, SellerViewModel sellerViewModel)
        {
            if (ModelState.IsValid)
            {
                sellerViewModel.Seller.SellerId = sellerId;
                var result = await _sellerBusinessService.UpdateSeller(sellerViewModel.Seller);
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
            return View(sellerViewModel);
        }

        [HttpPost]
        [Route("List")]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            try
            {
                var data = await _sellerBusinessService.RetrieveSellers(orderBy, paging);
                return this.JsonNet(data);
            }
            catch (Exception ex)
            {
                return this.JsonNet(""); ;
            }

        }

        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult> Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(await _sellerBusinessService.Search(searchKeyword, orderBy, paging));
        }

        [HttpPost]
        [Route("UpdateSellerApprovalState")]
        public async Task<ActionResult> UpdateSellerApprovalState(int sellerId)
        {
            var sellerdata = await _sellerBusinessService.RetrieveSeller(sellerId);
            sellerdata.ApprovalStateId = (int)SellerApprovalState.Approved;
            var data = await _sellerBusinessService.UpdateSellerApprovalState(sellerdata);
            return this.JsonNet(data);

        }
    }
}