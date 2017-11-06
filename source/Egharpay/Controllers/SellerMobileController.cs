using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
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
    }
}