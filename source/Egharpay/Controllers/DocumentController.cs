using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Interfaces;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using System.IO;
using Egharpay.Document.Interfaces;
using Egharpay.Models;

namespace Egharpay.Controllers
{
    public class DocumentController : BaseController
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IEgharpayBusinessService EgharpayBusinessService, IDocumentService documentService) : base(EgharpayBusinessService)
        {
            _documentService = documentService;
        }
        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentDocument(string studentCode, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(EgharpayBusinessService.RetrieveDocuments(UserOrganisationId, e => e.CentreId == UserCentreId && e.StudentCode == studentCode, orderBy, paging));
        }

        [HttpPost]
        public ActionResult List(Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(null);
        }

        public ActionResult Download(Guid id)
        {
            var document = EgharpayBusinessService.RetrieveDocument(UserOrganisationId, id);
            return File(System.IO.File.ReadAllBytes(document.Location), "application/pdf", document.FileName);
        }

        [HttpPost]
        public ActionResult DocumentTypes()
        {
            return this.JsonNet(EgharpayBusinessService.RetrieveDocumentTypes(UserOrganisationId));
        }

        [HttpPost]
        public void CreateDocument(DocumentViewModel documentViewModel)
        {
            
        }
    }
}