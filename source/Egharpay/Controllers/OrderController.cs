using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Configuration.Interface;
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
    [RoutePrefix("SellerOrders")]
    [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.Seller, Role.Personnel })]
    public class OrderController : BaseController
    {
        private readonly IOrderBusinessService _orderBusinessService;
        private readonly IPersonnelBusinessService _personnelBusinessService;
        public OrderController(IOrderBusinessService orderBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService, IPersonnelBusinessService personnelBusinessService) : base(configurationManager, authorizationService)
        {
            _orderBusinessService = orderBusinessService;
            _personnelBusinessService = personnelBusinessService;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            var orderResult = await _orderBusinessService.RetrieveOrders();
            var viewModel = new OrderViewModel()
            {
                Order = new Order()
            };
            return View(viewModel);
        }

        [PolicyAuthorize(Roles = new[] { Role.SuperUser })]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int? mobileId)
        {
            if (mobileId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = new OrderViewModel()
            {
                MobileId = mobileId.Value,
                CreatedDate = DateTime.UtcNow
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("RequestOrder")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Personnel })]
        public async Task<ActionResult> RequestOrder(int mobileId, int sellerId)
        {
            try
            {
                var personnelId = UserPersonnelId;
                return this.JsonNet(await _orderBusinessService.CreateOrder(mobileId, personnelId, sellerId));
            }
            catch (Exception e)
            {
                return this.JsonNet("");
            }
        }

        [HttpPost]
        [Route("List")]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var personnelId = UserPersonnelId;
            var personnel = await _personnelBusinessService.RetrievePersonnel(personnelId);
            if (User.IsSeller())
            {
                return this.JsonNet(await _orderBusinessService.RetrieveSellerOrders(e => e.SellerPersonnelId == personnel.Entity.PersonnelId, orderBy, paging));
            }
            if (User.IsSuperUser())
            {
                return this.JsonNet(await _orderBusinessService.RetrieveSellerOrders(e => true, orderBy, paging));
            }
            return this.JsonNet(await _orderBusinessService.RetrieveSellerOrders(e => e.BuyerPersonnelId == personnel.Entity.PersonnelId, orderBy, paging));
        }

        [HttpPost]
        [Route("Search")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.Seller })]
        public async Task<ActionResult> Search(string searchTerm, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(await _orderBusinessService.Search(searchTerm, orderBy, paging));
        }


        [HttpPost]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.Seller, Role.Personnel })]
        public async Task<ActionResult> UpdateOrder(int orderId)
        {
            var orderData = await _orderBusinessService.RetrieveOrder(orderId);
            var data = await _orderBusinessService.UpdateOrder(orderData);
            return this.JsonNet(data);
        }

        public ActionResult ViewOrder()
        {
            return View();
        }

        [Route("{orderId:int}/Edit")]
        public async Task<ActionResult> Edit(int orderId)
        {
            var order = await _orderBusinessService.RetrieveOrder(orderId);
            var model = new OrderViewModel()
            {
                Order = order
            };
            return View(model);
        }

        [HttpPost]
        [Route("{orderId:int}/UpdateShippingAddress")]
        public async Task<ActionResult> UpdateShippingAddress(int orderId, int shippingAddressId)
        {
            var order = await _orderBusinessService.RetrieveOrder(orderId);
            order.ShippingAddressId = shippingAddressId;
            await _orderBusinessService.UpdateOrder(order);
            return this.JsonNet("");
        }

        [HttpPost]
        [Route("SearchByDate")]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.MobileRepairAdmin })]
        public async Task<ActionResult> SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(await _orderBusinessService.RetrieveSellerOrders(e => e.OrderCreatedDate >= fromDate && e.OrderCreatedDate <= toDate, orderBy, paging));
        }
    }
}