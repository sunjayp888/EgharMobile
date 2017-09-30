using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Extensions;
using Egharpay.Models;


namespace Egharpay.Controllers
{
   // [Authorize]
    public class HomeController : BaseController
    {
        public HomeController() : base()
        {
        }

        public ActionResult Index()

        {
            bool isSuperAdmin = User.IsSuperAdmin();
            if (User.IsInRole("User"))
                return RedirectToAction("Profile", "Personnel");

            var viewModel = new HomeViewModel
            {
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(new BaseViewModel());
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(new BaseViewModel());
        }

        [HttpPost]
        public ActionResult GetCentres()
        {
            return this.JsonNet(null);
        }

        [HttpPost]
        public ActionResult Search()
        {
            return this.JsonNet(null);
        }
    }
}