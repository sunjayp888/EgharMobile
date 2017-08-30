using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Egharpay.Controllers
{
    public class CentreController : BaseController
    {
        private readonly IEgharpayBusinessService _EgharpayBusinessService;

        public CentreController(IEgharpayBusinessService EgharpayBusinessService) : base(EgharpayBusinessService)
        {
            _EgharpayBusinessService = EgharpayBusinessService;
        }
        // GET: Centre
       // [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Centre/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var organisationId = UserOrganisationId;
            var viewModel = new CentreViewModel
            {
                Centre = new Centre
                {
                }
            };
            return View(viewModel);
        }

        // POST: Centre/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CentreViewModel centreViewModel)
        {
            var organisationId = UserOrganisationId;
            if (ModelState.IsValid)
            {
                centreViewModel.Centre = EgharpayBusinessService.CreateCentre(UserOrganisationId, centreViewModel.Centre);
                return RedirectToAction("Index");
            }
            return View(centreViewModel);
        }

        // GET: Centre/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var centre = EgharpayBusinessService.RetrieveCentre(UserOrganisationId, id.Value);
            if (centre == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CentreViewModel
            {
                Centre = centre

            };
            return View(viewModel);
        }

        // POST: Centre/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CentreViewModel centreViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var viewModel = new CentreViewModel
            {
                Centre = centreViewModel.Centre
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(EgharpayBusinessService.RetrieveCentres(UserOrganisationId, orderBy, paging));
        }

    }
}