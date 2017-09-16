using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.Interfaces;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Controllers
{
    public class TrendCommentController : BaseController
    {
        private readonly ITrendCommentBusinessService _trendCommentBusinessService;

        public TrendCommentController(ITrendCommentBusinessService trendCommentBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService) : base(configurationManager, authorizationService)
        {
            _trendCommentBusinessService = trendCommentBusinessService;
        }

        // GET: TrendComment
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: TrendComment/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            var viewModel = new TrendCommentViewModel()
            {
                TrendComment = new TrendComment()
            };
            return View(viewModel);
        }

        // POST: TrendComment/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TrendCommentViewModel trendCommentViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create Trend
                trendCommentViewModel.TrendComment.CreatedDateTime = DateTime.UtcNow;
                var result = await _trendCommentBusinessService.CreateTrendComment(trendCommentViewModel.TrendComment);
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
            return View(trendCommentViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _trendCommentBusinessService.RetrieveTrendComments(orderBy, paging);
            return this.JsonNet(data);
        }
    }
}