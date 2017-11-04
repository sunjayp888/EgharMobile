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
using Role = Egharpay.Enums.Role;

namespace Egharpay.Controllers
{
    
    public class HomeBannerController : BaseController
    {
        private readonly IHomeBannerBusinessService _homeBannerBusinessService;
        private readonly IMobileBusinessService _mobileBusinessService;
        private readonly IDocumentsBusinessService _documentsBusinessService;

        public HomeBannerController(IHomeBannerBusinessService homeBannerBusinessService, IMobileBusinessService mobileBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService, IDocumentsBusinessService documentsBusinessService, IHomeBannerDocumentBusinessService homeBannerDocumentBusinessService) : base(configurationManager, authorizationService)
        {
            _homeBannerBusinessService = homeBannerBusinessService;
            _mobileBusinessService = mobileBusinessService;
            _documentsBusinessService = documentsBusinessService;
        }

        // GET: HomeBanner
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: HomeBanner/Create
        public async Task<ActionResult> Create()
        {
            var viewModel = new HomeBannerViewModel()
            {
                HomeBanner = new HomeBanner(),
            };
            return View(viewModel);
        }

        // POST: HomeBanner/Create
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin })]
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
                HomeBannerId = (int)id,
                Mobiles = new SelectList(mobiles, "MobileId", "Name")
            };
            return View(viewModel);
        }

        // POST: HomeBanner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin })]
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
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin })]
        public async Task<ActionResult> UploadPhoto(int? id)
        {
            try
            {
                var getHomeBannerResult = await _homeBannerBusinessService.RetrieveHomeBanner(id.Value);
                if (!getHomeBannerResult.Succeeded)
                    return HttpNotFound(string.Join(";", getHomeBannerResult.Errors));

                var homeBanner = getHomeBannerResult.Entity;

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

                        var result = await _homeBannerBusinessService.CreateHomeBannerImage(documentMeta, homeBanner.HomeBannerId);
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
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin })]
        [Route("HomeBanner/List")]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _homeBannerBusinessService.RetrieveHomeBanners(orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        [Route("HomeBanner/RetrieveHomeBannerDisplayImage")]
        public async Task<ActionResult> RetrieveHomeBannerDisplayImage(string pincode = null)
        {
            if (!string.IsNullOrEmpty(pincode))
            {
                var data = await _homeBannerBusinessService.RetrieveHomeBannerImages(e => e.Pincode == pincode);
                return this.JsonNet(data);
            }
            return this.JsonNet(await _homeBannerBusinessService.RetrieveHomeBannerImages(e => true));
        }

        [HttpPost]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin })]
        [Route("HomeBanner/RetrieveHomeBannerImageList")]
        public async Task<ActionResult> RetrieveHomeBannerImageList(int? homeBannerId, Paging paging, List<OrderBy> orderBy)
        {
            var data = await _homeBannerBusinessService.RetrieveHomeBannerImages(e => e.HomeBannerId == homeBannerId, orderBy, paging);
            return this.JsonNet(data);
        }


        [HttpPost]
        public async Task<ActionResult> DeleteHomeBannerDocument(Guid? guid)
        {
            //Pass documentDetailId
            //And In HomeBannerBusinessService Add method DeleteHomeBannerImage
            //In that method first delete HomeBannerDocument and then Document(guid)

            var guidList = new List<Guid> { guid.Value };
            var data = await _documentsBusinessService.DeleteDocument(guidList);
            return this.JsonNet(data);
        }
    }
}
