using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Services;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderBusinessService _orderBusinessService;
        public OrderController(IOrderBusinessService orderBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService) : base(configurationManager, authorizationService)
        {
            _orderBusinessService = orderBusinessService;
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
        public async Task<ActionResult> RequestMobile(List<Order> mobiles, int sellerId)
        {
            if (mobiles == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                foreach (var mobile in mobiles)
                {
                    mobile.CreatedDate = DateTime.UtcNow;
                    mobile.RequestTypeId = 1;
                    mobile.PersonnelId = UserPersonnelId;
                    return this.JsonNet(await _orderBusinessService.CreateOrder(mobile, sellerId));
                }
            }
            catch (Exception e)
            {
                return this.JsonNet("");
            }
            return this.JsonNet("");
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _orderBusinessService.RetrieveSellerOrders(e => true, orderBy, paging);
            return this.JsonNet(data);
        }

        public ActionResult ViewOrder()
        {
            return View();
        }
    }
}