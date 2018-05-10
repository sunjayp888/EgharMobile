using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Egharpay.Models.Authorization;
using Microsoft.Owin.Security.Authorization;
using Role = Egharpay.Enums.Role;

namespace Egharpay.Controllers
{
   
    public class TrendController : BaseController
    {
        private readonly ITrendBusinessService _trendBusinessService;

        public TrendController(ITrendBusinessService trendBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService) : base(configurationManager, authorizationService)
        {
            _trendBusinessService = trendBusinessService;
        }

        // GET: Trend
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Trend/Create
        [PolicyAuthorize(Roles = new[] { Role.SuperUser })]
        public async Task<ActionResult> Create()
        {
            var viewModel = new TrendViewModel()
            {
                Trend = new Trend()
            };
            return View(viewModel);
        }

        // POST: Trend/Create
        [PolicyAuthorize(Roles = new[] { Role.SuperUser})]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TrendViewModel trendViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create Trend
                trendViewModel.Trend.CreatedDateTime = DateTime.UtcNow;
                var result = await _trendBusinessService.CreateTrend(trendViewModel.Trend);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Exception);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(trendViewModel);
        }

        // GET: Trend/View/{id}
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var result = await _mobileBusinessService.RetrieveMobile(id.Value);
            var viewModel = new TrendViewModel
            {
                TrendId = id.Value
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _trendBusinessService.RetrieveTrends(orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> TrendData(int id)
        {
            var data = await _trendBusinessService.RetrieveTrend(id);
            return this.JsonNet(data);
        }
    }
}