using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Egharpay.Models.Authorization;
using Microsoft.Owin.Security.Authorization;
using DocumentCategory = Egharpay.Business.Enum.DocumentCategory;
using Role = Egharpay.Enums.Role;   
using Egharpay.Business.Enum;
using Egharpay.Entity;
using Egharpay.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Egharpay.Controllers
{
    [RoutePrefix("Personnel")]
    [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Personnel, Role.MobileRepairAdmin, Role.Seller })]
    public class PersonnelController : BaseController
    {
        private readonly IPersonnelBusinessService _personnelBusinessService;
        private readonly IPersonnelDocumentBusinessService _personnelDocumentBusinessService;
        private readonly IDocumentsBusinessService _documentsBusinessService;
        private readonly ISellerBusinessService _sellerBusinessService;

        // protected IAuthorizationService AuthorizationService { get; private set; }
        const string UserNotExist = "User does not exist.";

        public PersonnelController(IPersonnelBusinessService personnelBusinessService, IPersonnelDocumentBusinessService personnelDocumentBusinessService, IAuthorizationService authorizationService, IDocumentsBusinessService documentsBusinessService, ISellerBusinessService sellerBusinessService)
            : base(authorizationService)
        {
            _personnelBusinessService = personnelBusinessService;
            _personnelDocumentBusinessService = personnelDocumentBusinessService;
            _documentsBusinessService = documentsBusinessService;
            _sellerBusinessService = sellerBusinessService;
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationRoleManager _roleManager;

        private ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            set
            {
                _roleManager = value;
            }
        }


        // GET: Personnel
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Personnel/Profile/{id}
        [PolicyAuthorize(Roles = new[] { Role.Personnel, Role.Seller, Role.MobileRepairAdmin })]
        public async Task<ActionResult> Profile(bool? profileUpdated)
        {
            var id = UserPersonnelId;
            if (User.IsPersonnel() && !await AuthorizationService.AuthorizeAsync((ClaimsPrincipal)User, id, Policies.Resource.Personnel.ToString()))
                return HttpForbidden();

            if (id == 0)
                return RedirectToAction("Login", "Account");

            var personnel = await _personnelBusinessService.RetrievePersonnel(id);
            if (personnel == null)
                return HttpNotFound();

            var viewModel = new PersonnelProfileViewModel
            {
                Personnel = personnel.Entity,
                PersonnelId = personnel.Entity.PersonnelId,
                ProfileUpdated = profileUpdated ?? false
                //Permissions = EgharpayBusinessService.RetrievePersonnelPermissions(isAdmin, UserOrganisationId, UserPersonnelId, id),
                //PhotoBytes = EgharpayBusinessService.RetrievePhoto(organisationId, id)
            };
            if (User.IsSeller())
            {
                var seller = await _sellerBusinessService.RetrieveSellerByPersonnelId(personnel.Entity.PersonnelId);
                viewModel.IsSellerApproved = seller.ApprovalStateId == (int)SellerApprovalState.Approved;
            }

            return View(viewModel);
        }


        // GET: Personnel/Create
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin })]
        public ActionResult Create()
        {
            var personnelProfileViewModel = new PersonnelProfileViewModel()
            {
                Personnel = new Personnel()
            };
            return View(personnelProfileViewModel);
        }

        // POST: Personnel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin })]
        public async Task<ActionResult> Create(PersonnelProfileViewModel personnelViewModel)
        {
            var userExists = UserManager.FindByEmail(personnelViewModel.Personnel.Email);
            if (userExists != null)
                ModelState.AddModelError("", string.Format("An account already exists for the email address {0}", personnelViewModel.Personnel.Email));

            if (ModelState.IsValid)
            {
                //Create Personnel
                var result = await _personnelBusinessService.CreatePersonnel(personnelViewModel.Personnel);
                if (result.Succeeded)
                {
                     CreateUserAndRole(personnelViewModel.Personnel);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Exception);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(personnelViewModel);
        }

        private IdentityResult CreateUserAndRole(Personnel personnel)
        {
            var createUser = new ApplicationUser
            {
                UserName = personnel.Email,
                Email = personnel.Email,
            };

            var roleId = RoleManager.Roles.First(r => r.Name == Role.Admin.ToString()).Id;
            createUser.Roles.Add(new IdentityUserRole { UserId = createUser.Id, RoleId = roleId });

            var result = UserManager.Create(createUser, "Password1!");
            return result;
        }

        //[Authorize(Roles = "Admin,User")]
        //public async Task<ActionResult> Edit(int id)
        //{
        //    var personnel = await _personnelBusinessService.RetrievePersonnel(id);
        //    if (personnel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    //   var centres = EgharpayBusinessService.RetrieveCentres(UserOrganisationId, e => true);
        //    // personnel.Email = UserManager.FindByPersonnelId(personnel.PersonnelId)?.Email;
        //    var viewModel = new PersonnelProfileViewModel
        //    {
        //        //        Centres = new SelectList(centres, "CentreId", "Name"),
        //        Personnel = personnel
        //    };
        //    return View(viewModel);
        //}

        [HttpGet]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Personnel, Role.Seller, Role.MobileRepairAdmin })]
        [Route("{personnelId:int}/Edit")]
        public async Task<ActionResult> Edit(int personnelId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var personnel = await _personnelBusinessService.RetrievePersonnel(personnelId);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            var viewModel = new PersonnelProfileViewModel
            {
                Personnel = personnel.Entity
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{personnelId:int}/Edit")]
        [PolicyAuthorize(Roles = new[] { Role.Personnel, Role.Seller, Role.MobileRepairAdmin, Role.SuperUser })]
        public async Task<ActionResult> Edit(PersonnelProfileViewModel personnelViewModel)
        {
            if (ModelState.IsValid)
            {
                var resultData = await _personnelBusinessService.UpdatePersonnel(personnelViewModel.Personnel);
                if (resultData.Succeeded)
                {
                    personnelViewModel.ProfileUpdated = resultData.Succeeded;
                    return RedirectToAction("Profile", new { profileUpdated = personnelViewModel.ProfileUpdated });
                }
                ModelState.AddModelError("", resultData.Exception);
                foreach (var error in resultData.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            var viewModel = new PersonnelProfileViewModel
            {
                Personnel = personnelViewModel.Personnel
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route("{personnelId:int}/UploadPhoto")]
        public async Task<ActionResult> UploadPhoto(int personnelId)
        {
            if (!await AuthorizationService.AuthorizeAsync((ClaimsPrincipal)User, personnelId, Policies.Resource.Personnel.ToString()))
                return HttpForbidden();

            try
            {
                var getPersonnelResult = await _personnelBusinessService.RetrievePersonnel(personnelId);
                if (!getPersonnelResult.Succeeded)
                    return HttpNotFound(string.Join(";", getPersonnelResult.Errors));

                var person = getPersonnelResult.Entity;

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        //var personnelProfile = await _personnelDocumentBusinessService.RetrievePersonnelProfileImage(getPersonnelResult.Entity.PersonnelId);
                        //if (personnelProfile.Succeeded)
                        //    await _documentsBusinessService.DeleteDocument(new List<Guid> { personnelProfile.Entity.DocumentGuid.Value });

                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(file.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(file.ContentLength);
                        }
                        var documentMeta = new Document()
                        {
                            Content = fileData,
                            Description = string.Format("{0} Profile Image", person.FullName),
                            FileName = file.FileName.Split('\\').Last() + ".png",
                            PersonnelName = person.FullName,
                            CreatedBy = User.Identity.Name,
                            PersonnelId = person.PersonnelId.ToString(),
                            Category = Business.Enum.DocumentCategory.ProfilePhoto.ToString(),
                            CategoryId = (int)DocumentCategory.ProfilePhoto
                        };

                        var result = await _documentsBusinessService.CreateDocument(documentMeta);
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
        public ActionResult DeletePhoto(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //EgharpayBusinessService.DeletePhoto(UserOrganisationId, id.Value);
                return this.JsonNet("");
            }
            catch (Exception ex)
            {
                return this.JsonNet(ex);
            }

        }

        [HttpPost]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser })]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _personnelBusinessService.RetrievePersonnels(orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser })]
        public async Task<ActionResult> Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            var data = await _personnelBusinessService.Search(searchKeyword, orderBy, paging);
            return this.JsonNet(data);
        }

        [Route("{personnelId:int}/Documents")]
        public async Task<ActionResult> Documents(int? personnelId)
        {
            //if (!await AuthorizationService.AuthorizeAsync((ClaimsPrincipal)User, personnelId.Value, Policies.Resource.Personnel.ToString()))
            //    return HttpForbidden();

            var personnel = await _personnelBusinessService.RetrievePersonnel(personnelId.Value);
            if (personnel == null)
                return HttpNotFound(UserNotExist);

            return View();
        }

        [Route("RetrieveProfileImage/{personnelId:int}")]
        public async Task<ActionResult> RetrieveProfileImage(int personnelId)
        {
            if (!await AuthorizationService.AuthorizeAsync((ClaimsPrincipal)User, personnelId, Policies.Resource.Personnel.ToString()))
                return HttpForbidden();

            var personnels = await _personnelDocumentBusinessService.RetrievePersonnelDocuments(personnelId, DocumentCategory.ProfilePhoto);
            if (personnels == null)
                return HttpNotFound(UserNotExist);

            var profileImage = personnels.Entity.FirstOrDefault();

            return this.JsonNet(profileImage);
        }
    }
}