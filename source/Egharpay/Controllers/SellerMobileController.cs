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

namespace Egharpay.Controllers
{
    public class SellerMobileController : BaseController
    {
        private readonly ISellerMobileBusinessService _sellerMobileBusinessService;

        public SellerMobileController(ISellerMobileBusinessService sellerMobileBusinessService)
        {
            _sellerMobileBusinessService = sellerMobileBusinessService;
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
            var data = await _sellerMobileBusinessService.RetrieveSellerMobileGrids(e=>true, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(await _sellerMobileBusinessService.Search(searchKeyword, orderBy, paging));
        }

        [HttpPost]
        public ActionResult SearchByDate(DateTime fromDate, DateTime toDate, Paging paging, List<OrderBy> orderBy)
        {
            bool isSuperAdmin = User.IsInAnyRoles("SuperAdmin");
            return this.JsonNet(_sellerMobileBusinessService.RetrieveSellerMobileGrids(e => e.AppointmentDate >= fromDate && e.AppointmentDate <= toDate, orderBy, paging));
        }
    }
}