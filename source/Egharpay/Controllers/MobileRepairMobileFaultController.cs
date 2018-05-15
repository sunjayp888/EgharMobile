using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Extensions;
using Egharpay.Models;
using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Controllers
{
    public class MobileRepairMobileFaultController : BaseController
    {
        private readonly IMobileRepairMobileFaultBusinessService _mobileRepairMobileFaultBusinessService;

        public MobileRepairMobileFaultController(IMobileRepairMobileFaultBusinessService mobileRepairMobileFaultBusinessService, IAuthorizationService authorizationService) : base(authorizationService)
        {
            _mobileRepairMobileFaultBusinessService = mobileRepairMobileFaultBusinessService;
        }
        // GET: MobileRepairMobileFault
        public ActionResult Index()
        {
            return View();
        }

        // POST: MobileRepairMobileFault/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(MobileRepairMobileFault mobileRepairMobileFault)
        //{
        //    var result = await _mobileRepairMobileFaultBusinessService.CreateMobileRepairMobileFault(mobileRepairMobileFault);
        //    return this.JsonNet(result);
        //}

        [HttpPost]
        public async Task<ActionResult> List(int mobileRepairId)
        {
            var data = await _mobileRepairMobileFaultBusinessService.RetrieveMobileRepairMobileFaults(mobileRepairId);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int mobileRepairId, int mobileFaultId)
        {
            try
            {
                var result = await _mobileRepairMobileFaultBusinessService.DeleteMobileRepairMobileFault(mobileRepairId, mobileFaultId);
                return this.JsonNet(result);
            }
            catch (Exception ex)
            {
                return this.JsonNet(ex);
            }

        }
    }
}