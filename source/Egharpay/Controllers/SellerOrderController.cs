using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.Interfaces;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models.Authorization;
using Microsoft.Owin.Security.Authorization;
using Role = Egharpay.Enums.Role;

namespace Egharpay.Controllers
{
    [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.Seller })]
    public class SellerOrderController : BaseController
    {
        private readonly ISellerOrderBusinessService _sellerOrderBusinessService;

        public SellerOrderController(ISellerOrderBusinessService sellerOrderBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService) : base(configurationManager, authorizationService)
        {
            _sellerOrderBusinessService = sellerOrderBusinessService;
        }

        // GET: SellerOrder
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _sellerOrderBusinessService.RetrieveSellerOrders(e => true, orderBy, paging);
            return this.JsonNet(data);
        }
    }
}