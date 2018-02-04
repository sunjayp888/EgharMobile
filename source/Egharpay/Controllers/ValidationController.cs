using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Egharpay.Controllers
{
    public class ValidationController : Controller
    {
        // GET: Validation
        public ActionResult Index()
        {
            return View();
        }

        //Compare is not working :(
        public JsonResult CompareConfirmPassword(string confirmPassword, string password)
        {
            return Json(password.ToLower() == confirmPassword.ToLower(), JsonRequestBehavior.AllowGet);
        }
    }
}