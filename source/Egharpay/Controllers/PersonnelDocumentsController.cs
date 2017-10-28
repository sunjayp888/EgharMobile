using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Interfaces;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;

namespace Egharpay.Controllers
{
    public class PersonnelDocumentsController : Controller
    {
        // GET: PersonnelDocuments
        private readonly IDocumentsBusinessService _documentBusinessService;
        private IPersonnelBusinessService _personnelBusinessService;

        public PersonnelDocumentsController(IDocumentsBusinessService documentBusinessService, IPersonnelBusinessService personnelBusinessService)
        {
            _documentBusinessService = documentBusinessService;
            _personnelBusinessService = personnelBusinessService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Route("PersonnelDocument/{personnelId}/UploadDocumentModal")]
        public ActionResult UploadDocumentModal(int personnelId)
        {
            var model = new PersonnelDocumentViewModel { PersonnelId = personnelId };
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
                return this.JsonNet(await _personnelBusinessService.RetrievePersonnelDocuments(personnelId, paging, orderBy));
            }
            catch (Exception ex)
            {
                return this.JsonErrorResult(ex);
            }
        }

    }
}