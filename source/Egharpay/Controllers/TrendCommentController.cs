using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
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
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> Create()
        //{
        //    var viewModel = new TrendCommentViewModel()
        //    {
        //        TrendComment = new TrendComment()
        //    };
        //    return View(viewModel);
        //}

        [HttpPost]
        public async Task<ActionResult> Create(TrendComment trendComment)
        {
            var validationresult = new ValidationResult();
            if (!User.Identity.IsAuthenticated)
            {
                validationresult.Succeeded = false;
                validationresult.Message = "Invalid Login.";
                return this.JsonNet(validationresult);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    //Create Trend
                    trendComment.PersonnelId = UserPersonnelId;
                    var result = await _trendCommentBusinessService.CreateTrendComment(trendComment);
                    if (result.Succeeded)
                    {
                        validationresult.Succeeded = true;
                        return this.JsonNet(validationresult);
                    }
                }
                return this.JsonNet(validationresult);
            }
            catch (Exception e)
            {
                return this.JsonNet(validationresult);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Approve(int trendCommentId)
        {
            var trendCommentData = await _trendCommentBusinessService.RetrieveTrendComment(trendCommentId);
            trendCommentData.Approve = true;
            await _trendCommentBusinessService.UpdateTrendComment(trendCommentData);
            return this.JsonNet(trendCommentData);
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _trendCommentBusinessService.RetrieveTrendComments(orderBy, paging);
            return this.JsonNet(data);
        }
    }
}