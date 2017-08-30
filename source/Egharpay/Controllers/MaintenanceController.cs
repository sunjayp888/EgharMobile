using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Interfaces;
using Egharpay.Models;

namespace Egharpay.Controllers
{
    public class MaintenanceController : BaseController
    {
        private IMaintenanceBusinessService _maintenanceBusinessService;
        private IEgharpayBusinessService _egharpayBusinessService;

        public MaintenanceController(IMaintenanceBusinessService maintenanceBusinessService, IEgharpayBusinessService egharpayBusinessService) : base(egharpayBusinessService)
        {
            _maintenanceBusinessService = maintenanceBusinessService;
            _egharpayBusinessService = egharpayBusinessService;
        }

        // GET: Maintenance
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }
    }
}