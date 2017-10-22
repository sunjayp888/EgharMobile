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
        public async Task<ActionResult> RequestMobile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = new Order()
            {
                MobileId = id.Value,
                CreatedDate = DateTime.UtcNow,
                RequestTypeId = 1,
            };
            var result = await _orderBusinessService.CreateOrder(order);
            return this.JsonNet(result);
        }
    }
}