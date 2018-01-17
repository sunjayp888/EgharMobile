using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Extensions;
using Microsoft.AspNet.Identity;

namespace Egharpay.Controllers
{
    public class AddressController : BaseController
    {
        private readonly IPersonnelBusinessService _personnelBusinessService;
        private readonly IAddressBusinessService _addressBusinessService;

        public AddressController(IPersonnelBusinessService personnelBusinessService, IAddressBusinessService addressBusinessService)
        {
            _personnelBusinessService = personnelBusinessService;
            _addressBusinessService = addressBusinessService;
        }

        // GET: Address
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Address/Create")]
        public async Task<ActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {
                var personnel = await _personnelBusinessService.RetrievePersonnel(User.Identity.GetUserId());
                if (personnel == null)
                    return RedirectToAction("Login", "Account");
                var result = await _addressBusinessService.CreateAddress(personnel.PersonnelId, address);
                if (result.Succeeded)
                    return this.JsonNet(string.Empty);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return this.JsonNet(
                ModelState.Values.Where(e => e.Errors.Count > 0)
                    .Select(e => e.Errors.Select(d => d.ErrorMessage).FirstOrDefault())
                    .Distinct());
        }

        [HttpGet]
        [Route("Address/RetrievePersonnelAddress")]
        public async Task<ActionResult> RetrievePersonnelAddress()
        {
            var personnel = await _personnelBusinessService.RetrievePersonnel(User.Identity.GetUserId());
            if (personnel == null)
                return RedirectToAction("Login", "Account");
            var result = await _addressBusinessService.RetrieveAddresses(personnel.PersonnelId);
            return this.JsonNet(result.ToList());
        }
    }
}