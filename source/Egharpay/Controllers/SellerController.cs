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
using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Controllers
{
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

        // GET: Seller/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            var sellerResult = await _sellerBusinessService.RetrieveSellers();
            var viewModel = new SellerViewModel()
            {
                Seller = new Seller()
            };
            return View(viewModel);
        }

        // POST: Seller/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SellerViewModel sellerViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create Seller
                sellerViewModel.Seller.CreatedDate = DateTime.UtcNow;
                sellerViewModel.Seller.ApprovalStateId = (int) ApprovalState.Pending;
                var result = await _sellerBusinessService.CreateSeller(sellerViewModel.Seller);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                //ModelState.AddModelError("", result.Exception);
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError("", error);
                //}
            }
            return View(sellerViewModel);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var seller = await _sellerBusinessService.RetrieveSeller(id.Value);
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

        // POST: Seller/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SellerViewModel sellerViewModel)
        {
            if (ModelState.IsValid)
            {
                //var result = await _shopBusinessService.UpdateApartment(apartmentViewModel.Apartment);
                //if (result.Succeeded)
                //{
                //    return RedirectToAction("Index");
                //}
                //ModelState.AddModelError("", result.Exception);
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError("", error);
                //}
            }
            return View(sellerViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            try
            {
                var data = await _sellerBusinessService.RetrieveSellers(orderBy, paging);
                return this.JsonNet(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(await _sellerBusinessService.Search(searchKeyword, orderBy, paging));
        }

        [HttpPost]
        public async Task<ActionResult> UpdateSellerApprovalState(int sellerId)
        {
            var sellerdata = await _sellerBusinessService.RetrieveSeller(sellerId);
            sellerdata.ApprovalStateId = (int)ApprovalState.Approved;
            await _sellerBusinessService.UpdateSeller(sellerdata);
            return this.JsonNet(sellerdata);

        }
    }
}