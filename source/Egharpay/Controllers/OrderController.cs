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

        // POST: Order/Create
        [Authorize(Roles = "Admin")]
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
        [Route("Orders/RequestOrder")]
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
    }
}