﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Egharpay.Models.Authorization;
using Egharpay.Models.Identity;
using Microsoft.Owin.Security.Authorization;
using Role = Egharpay.Enums.Role;

namespace Egharpay.Controllers
{
    [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Personnel})]
    public class PersonnelController : BaseController
    {
        private readonly IPersonnelBusinessService _personnelBusinessService;
        private readonly IPersonnelDocumentBusinessService _personnelDocumentBusinessService;
        private readonly IDocumentsBusinessService _documentsBusinessService;

        // protected IAuthorizationService AuthorizationService { get; private set; }
        const string UserNotExist = "User does not exist.";

        public PersonnelController(IPersonnelBusinessService personnelBusinessService, IPersonnelDocumentBusinessService personnelDocumentBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService, IDocumentsBusinessService documentsBusinessService)
            : base(authorizationService)
        {
            _personnelBusinessService = personnelBusinessService;
            _personnelDocumentBusinessService = personnelDocumentBusinessService;
            _documentsBusinessService = documentsBusinessService;
        }

        // GET: Personnel
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Personnel/Profile/{id}
        //[PolicyAuthorize(Roles = new[] { Enum.Role.SuperUser, Enum.Role.Personnel })]
        public async Task<ActionResult> Profile(int? id)
        {
            if (!await AuthorizationService.AuthorizeAsync((ClaimsPrincipal)User, id, Policies.Resource.Personnel.ToString()))
                return HttpForbidden();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var personnel = await _personnelBusinessService.RetrievePersonnel(id.Value);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            var isAdmin = User.IsInAnyRoles("Admin");
            if (!isAdmin)
            {
            }
            var viewModel = new PersonnelProfileViewModel
            {
                Personnel = personnel.Entity,
                //Permissions = EgharpayBusinessService.RetrievePersonnelPermissions(isAdmin, UserOrganisationId, UserPersonnelId, id),
                //PhotoBytes = EgharpayBusinessService.RetrievePhoto(organisationId, id)
            };
            return View(viewModel);
        }


        // GET: Personnel/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var centres = _personnelBusinessService.RetrievePersonnel(1, 1);
            //var viewModel = new PersonnelProfileViewModel
            //{

            //    Centres = new SelectList(centres, "CentreId", "Name"),
            //    Personnel = new Personnel
            //    {
            //        DOB = DateTime.Today,
            //        Title = "Mr",
            //        Forenames = "A",
            //        Surname = "B",
            //        Email = string.Format("{0}@hr.com", Guid.NewGuid()),
            //        Address1 = "Address1",
            //        Postcode = "POST CODE",
            //        Telephone = "12345678",
            //    },
            //};
            return View();
        }

        // POST: Personnel/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PersonnelProfileViewModel personnelViewModel)
        {
            // check if user with this email already exists for the current organisation
            // var centres = EgharpayBusinessService.RetrieveCentres(UserOrganisationId, e => true);
            var userExists = UserManager.FindByEmail(personnelViewModel.Personnel.Email);
            // personnelViewModel.Centres = new SelectList(centres, "CentreId", "Name");
            if (userExists != null)
                ModelState.AddModelError("", string.Format("An account already exists for the email address {0}", personnelViewModel.Personnel.Email));

            if (ModelState.IsValid)
            {
                //Create Personnel
                var result = await _personnelBusinessService.CreatePersonnel(personnelViewModel.Personnel);
                if (result.Succeeded)
                {
                   // CreateUserAndRole(personnelViewModel.Personnel);
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

        //private IdentityResult CreateUserAndRole(Personnel personnel)
        //{
        //    var createUser = new ApplicationUser
        //    {
        //        UserName = personnel.Email,
        //        Email = personnel.Email,
        //    };

        //    var roleId = RoleManager.Roles.FirstOrDefault(r => r.Name == "User").Id;
        //    createUser.Roles.Add(new IdentityUserRole { UserId = createUser.Id, RoleId = roleId });

        //    var result = UserManager.Create(createUser, "Password1!");
        //    return result;
        //}

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

        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> Edit()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var personnel = await _personnelBusinessService.RetrievePersonnel(UserPersonnelId);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            //   var centres = EgharpayBusinessService.RetrieveCentres(UserOrganisationId, e => true);
            // personnel.Email = UserManager.FindByPersonnelId(personnel.PersonnelId)?.Email;
            var viewModel = new PersonnelProfileViewModel
            {
                //        Centres = new SelectList(centres, "CentreId", "Name"),
                Personnel = personnel.Entity
            };
            return View(viewModel);
        }

        // POST: Personnels/Edit/{id}
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PersonnelProfileViewModel personnelViewModel)
        {
            if (ModelState.IsValid)
            {

                var resultData = await _personnelBusinessService.UpdatePersonnel(personnelViewModel.Personnel);
                if (resultData.Succeeded)
                {
                    //var editUser = UserManager.FindByPersonnelId(personnelViewModel.Personnel.PersonnelId);
                    //editUser.Email = personnelViewModel.Personnel.Email;
                    //var result = UserManager.Update(editUser);
                    //if (result.Succeeded)
                    return RedirectToAction("Profile", new { id = personnelViewModel.Personnel.PersonnelId });
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
        public async Task<ActionResult> UploadPhoto(int? id)
        {
            try
            {
                var getPersonnelResult = await _personnelBusinessService.RetrievePersonnel(id.Value);
                if (!getPersonnelResult.Succeeded)
                    return HttpNotFound(string.Join(";", getPersonnelResult.Errors));

                var person = getPersonnelResult.Entity;

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var personnelProfile = await _personnelDocumentBusinessService.RetrievePersonnelProfileImage(getPersonnelResult.Entity.PersonnelId);
                        if (personnelProfile.Succeeded)
                            await _documentsBusinessService.DeleteDocument(new List<Guid> { personnelProfile.Entity.DocumentGuid.Value });

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
                            CategoryId = (int)Business.Enum.DocumentCategory.ProfilePhoto
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
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _personnelBusinessService.RetrievePersonnels(orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            //var data = await _personnelBusinessService.Search(1, searchKeyword, orderBy, paging);
            return this.JsonNet("");
        }

        [Route("{personnelId}/Documents")]
        public async Task<ActionResult> Documents(int? personnelId)
        {
            //if (!await AuthorizationService.AuthorizeAsync((ClaimsPrincipal)User, personnelId.Value, Policies.Resource.Personnel.ToString()))
            //    return HttpForbidden();

            var personnel = await _personnelBusinessService.RetrievePersonnel(personnelId.Value);
            if (personnel == null)
                return HttpNotFound(UserNotExist);

            return View();
        }
    }
}