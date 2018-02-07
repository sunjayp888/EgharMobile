using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Interfaces;

namespace Egharpay.Controllers
{
    public class ValidationController : Controller
    {
        private IPersonnelBusinessService _personnelBusinessService;

        public ValidationController(IPersonnelBusinessService personnelBusinessService)
        {
            _personnelBusinessService = personnelBusinessService;
        }

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

        public JsonResult IsValidUsername(string username)
        {
            var isValidUsername = new EmailAddressAttribute().IsValid(username) || (Extensions.StringExtensions.IsNumeric(username) && username.Length == 10);
            return Json(isValidUsername, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> PersonnelAlreadyExists(string mobile, string email)
        {
            var result = await _personnelBusinessService.PersonnelAlreadyExists(mobile, email);
            return Json(result.Succeeded, JsonRequestBehavior.AllowGet);
        }

    }
}