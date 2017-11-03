using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Egharpay.Models.Authorization;
using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Controllers
{
    public class HomeBannerController : BaseController
    {
        private readonly IHomeBannerBusinessService _homeBannerBusinessService;
        private readonly IMobileBusinessService _mobileBusinessService;
        private readonly IDocumentsBusinessService _documentsBusinessService;
        private readonly IHomeBannerDocumentBusinessService _homeBannerDocumentBusinessService;

        public HomeBannerController(IHomeBannerBusinessService homeBannerBusinessService, IMobileBusinessService mobileBusinessService,IConfigurationManager configurationManager, IAuthorizationService authorizationService, IDocumentsBusinessService documentsBusinessService, IHomeBannerDocumentBusinessService homeBannerDocumentBusinessService) : base(configurationManager, authorizationService)
        {
            _homeBannerBusinessService = homeBannerBusinessService;
            _mobileBusinessService = mobileBusinessService;
            _documentsBusinessService = documentsBusinessService;
            _homeBannerDocumentBusinessService = homeBannerDocumentBusinessService;
        }

        // GET: HomeBanner
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: HomeBanner/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            //var mobile = await _mobileBusinessService.RetrieveMobiles();
            //var mobiles = mobile.Items.ToList();
            var viewModel = new HomeBannerViewModel()
            {
                HomeBanner = new HomeBanner(),
                //Mobiles = new SelectList(mobiles, "MobileId", "Name")
            };
            return View(viewModel);
        }

        // POST: HomeBanner/Create
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HomeBannerViewModel homeBannerViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create HomeBanner
                var result = await _homeBannerBusinessService.CreateHomeBanner(homeBannerViewModel.HomeBanner);
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
            return View(homeBannerViewModel);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var homeBanner = await _homeBannerBusinessService.RetrieveHomeBanner(id.Value);
            var mobile = await _mobileBusinessService.RetrieveMobiles();
            var mobiles = mobile.Items.ToList();
            if (homeBanner == null)
            {
                return HttpNotFound();
            }
            var viewModel = new HomeBannerViewModel()
            {
                HomeBanner = homeBanner.Entity,
                HomeBannerId = (int) id,
                Mobiles = new SelectList(mobiles, "MobileId", "Name")
            };
            return View(viewModel);
        }

        // POST: HomeBanner/Create
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HomeBannerViewModel homeBannerViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create HomeBanner
                var result = await _homeBannerBusinessService.UpdateHomeBanner(homeBannerViewModel.HomeBanner);
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
            return View(homeBannerViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> UploadPhoto(int? id)
        {
            try
            {
                var getPersonnelResult = await _homeBannerBusinessService.RetrieveHomeBanner(id.Value);
                if (!getPersonnelResult.Succeeded)
                    return HttpNotFound(string.Join(";", getPersonnelResult.Errors));

                var homeBanner = getPersonnelResult.Entity;

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                       
                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(file.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(file.ContentLength);
                        }
                        var documentMeta = new Document()
                        {
                            Content = fileData,
                            Description = string.Format("{0} Home Banner Image", homeBanner.Name),
                            FileName = file.FileName.Split('\\').Last() + ".png",
                            PersonnelName = homeBanner.Name,
                            CreatedBy = User.Identity.Name,
                            PersonnelId = homeBanner.HomeBannerId.ToString(),
                            Category = Business.Enum.DocumentCategory.HomeBannerImage.ToString()
                        };

                        var result = await _homeBannerBusinessService.CreateHomeBannerImage(documentMeta,homeBanner.HomeBannerId);
                        if (!result.Succeeded)
                            return this.JsonNet("SaveError");
                    }
                }
                return this.JsonNet("");
            }
            catch (Exception ex)
            {
                return this.JsonNet(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _homeBannerBusinessService.RetrieveHomeBanners(orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> HomeBannerImage(DateTime startDateTime, DateTime endDateTime, string pincode)
        {
            var data = await _homeBannerBusinessService.RetrieveHomeBannerImages(startDateTime, endDateTime, pincode);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> HomeBannerImageDocument(int homeBannerId)
        {
            var data = await _documentsBusinessService.RetrieveDocuments(homeBannerId, Business.Enum.DocumentCategory.HomeBannerImage);
            return this.JsonNet(data.Entity);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteHomeBannerDocument(Guid? guid)
        {
            var guidList = new List<Guid> {guid.Value};
            var data = await _documentsBusinessService.DeleteDocument(guidList);
            return this.JsonNet(data);
        }
    }
}