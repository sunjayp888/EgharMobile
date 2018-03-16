using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Egharpay.Models.Authorization;
using Microsoft.AspNet.Identity;
using Role = Egharpay.Enums.Role;

namespace Egharpay.Controllers
{
    public class PersonnelDocumentController : BaseController
    {
        // GET: PersonnelDocuments
        private readonly IDocumentsBusinessService _documentBusinessService;
        private readonly IPersonnelBusinessService _personnelBusinessService;
        private readonly IPersonnelDocumentBusinessService _personnelDocumentBusinessService;

        public PersonnelDocumentController(IDocumentsBusinessService documentBusinessService, IPersonnelBusinessService personnelBusinessService, IPersonnelDocumentBusinessService personnelDocumentBusinessService)
        {
            _documentBusinessService = documentBusinessService;
            _personnelBusinessService = personnelBusinessService;
            _personnelDocumentBusinessService = personnelDocumentBusinessService;
        }

        public ActionResult Index()
        {
            return View();
        }
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.Personnel })]
        public ActionResult UploadDocumentModal(int? personnelId)
        {
            var model = new PersonnelDocumentViewModel { PersonnelId = personnelId.Value };
            return PartialView("Personnel/_PersonnelDocumentModal", model);
        }

        [Route("Worker/DocumentCategories")]
        public async Task<ActionResult> DocumentCategories()
        {
            try
            {
                var documentCategories = await _documentBusinessService.RetrieveDocumentCategoriesAsync();
                //var genericDocumentcategories = documentCategories.Where(category => category.GenericDocument);
                return this.JsonNet(documentCategories);
            }
            catch (Exception ex)
            {
                return this.JsonErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("{personnelId}/WorkerDocuments")]
        public async Task<ActionResult> WorkerDocuments(int personnelId, Paging paging = null, List<OrderBy> orderBy = null)
        {
            try
            {
                return this.JsonNet(await _personnelDocumentBusinessService.RetrievePersonnelDocuments(personnelId, paging, orderBy));
            }
            catch (Exception ex)
            {
                return this.JsonErrorResult(ex);
            }
        }

        [HttpPost]
        [PolicyAuthorize(Roles = new[] { Role.SuperUser, Role.Admin, Role.Personnel, Role.Seller })]
        [Route("PersonnelDocument/{id}/{tagLine}/UploadSelfie")]
        public async Task<ActionResult> UploadSelfie(int? id, string tagLine)
        {
            try
            {
                var getPersonnelResult = await _personnelBusinessService.RetrievePersonnel(id.Value);
                if (!getPersonnelResult.Succeeded)
                    return HttpNotFound(string.Join(";", getPersonnelResult.Errors));

                var personnel = getPersonnelResult.Entity;

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
                            Description = tagLine=="null" ? string.Empty : tagLine,
                            FileName = file.FileName.Split('\\').Last() + ".png",
                            PersonnelName = personnel.FullName,
                            CreatedBy = User.Identity.GetUserId(),
                            PersonnelId = personnel.PersonnelId.ToString(),
                            Category = Business.Enum.DocumentCategory.Selfie.ToString(),
                            CategoryId = (int)Business.Enum.DocumentCategory.Selfie
                        };

                        var result = await _personnelBusinessService.UploadDocument(documentMeta, personnel.PersonnelId);
                        if (!result.Succeeded)
                            return this.JsonNet("SaveError");
                    }
                }
                return this.JsonNet("");
            }
            catch (Exception ex)
            {
                return this.JsonNet("SaveError");
            }
        }

        [HttpPost]
        [Route("PersonnelDocument/RetrievePersonnelSelfieImages")]
        public async Task<ActionResult> RetrievePersonnelSelfieImages()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var data = await _personnelDocumentBusinessService.RetrievePersonnelSelfies(startDate, startDate.AddDays(30));//For now display one month uploaded selfie
            return this.JsonNet(data);
        }

    }
}