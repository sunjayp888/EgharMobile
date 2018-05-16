using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models.Authorization;
using Role = Egharpay.Enums.Role;

namespace Egharpay.Controllers
{
    [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.Seller })]
    public class SellerMobileController : BaseController
    {
        private readonly ISellerMobileBusinessService _sellerMobileBusinessService;
        private readonly ISellerBusinessService _sellerBusinessService;

        public SellerMobileController(ISellerMobileBusinessService sellerMobileBusinessService, ISellerBusinessService sellerBusinessService)
        {
            _sellerMobileBusinessService = sellerMobileBusinessService;
            _sellerBusinessService = sellerBusinessService;
        }

        // GET: SellerMobile
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AssignMobileToSeller(SellerMobile sellerMobile)
        {
            var data = await _sellerMobileBusinessService.AddMobileInStore(sellerMobile);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            if (User.IsSuperUserOrAdmin())
                return this.JsonNet(await _sellerMobileBusinessService.RetrieveSellerMobileGrids(e => true, orderBy, paging));

            var seller = await _sellerBusinessService.RetrieveSellerByPersonnelId(UserPersonnelId);
            var data = await _sellerMobileBusinessService.RetrieveSellerMobileGrids(e => e.SellerId == seller.SellerId, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            if (User.IsSuperUserOrAdmin())
                return this.JsonNet(await _sellerMobileBusinessService.RetrieveSellerMobileGrids(e => true, orderBy, paging));

            var seller = await _sellerBusinessService.RetrieveSellerByPersonnelId(UserPersonnelId);
            return this.JsonNet(await _sellerMobileBusinessService.Search(seller.SellerId, searchKeyword, orderBy, paging));
        }

        [HttpPost]
        public async Task<ActionResult> SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            if (User.IsSuperUserOrAdmin())
                return this.JsonNet(await _sellerMobileBusinessService.RetrieveSellerMobileGrids(e => true, orderBy, paging));

            var seller = await _sellerBusinessService.RetrieveSellerByPersonnelId(UserPersonnelId);
            var data = await _sellerMobileBusinessService.RetrieveSellerMobileGrids(e => e.SellerId == seller.SellerId, orderBy, paging);
            return this.JsonNet(data);
        }
    }
}