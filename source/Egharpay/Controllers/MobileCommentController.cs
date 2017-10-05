using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Controllers
{
    public class MobileCommentController : BaseController
    {
        private readonly IMobileCommentBusinessService _mobileCommentBusinessService;

        public MobileCommentController(IMobileCommentBusinessService mobileCommentBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService) : base(configurationManager, authorizationService)
        {
            _mobileCommentBusinessService = mobileCommentBusinessService;
        }

        // GET: MobileComment
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // POST: TrendComment/Create
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MobileComment mobileComment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Create Trend
                    mobileComment.UserId = 1;
                    mobileComment.CreatedDateTime = DateTime.UtcNow;
                    var result = await _mobileCommentBusinessService.CreateMobileComment(mobileComment);
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
                return this.JsonNet(true);
            }
            catch (Exception e)
            {
                return this.JsonNet(false);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Approve(int mobileCommentId)
        {
            var mobileCommentData = await _mobileCommentBusinessService.RetrieveMobileComment(mobileCommentId);
            mobileCommentData.Approve = true;
            await _mobileCommentBusinessService.UpdateMobileComment(mobileCommentData);
            return this.JsonNet(mobileCommentData);
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _mobileCommentBusinessService.RetrieveMobileComments(orderBy, paging);
            return this.JsonNet(data);
        }
    }
}