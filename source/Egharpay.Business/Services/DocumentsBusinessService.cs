using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data;
using Egharpay.Data.Interfaces;

namespace Egharpay.Business.Services
{
    public class DocumentsBusinessService : IDocumentsBusinessService
    {
        private readonly IMapper _mapper;
        private readonly IDocumentDataService _documentDataService;
        public DocumentsBusinessService(IMapper mapper, IDocumentDataService documentDataService)
        {
            _mapper = mapper;
            _documentDataService = documentDataService;
        }


        public async Task<IEnumerable<DocumentCategory>> RetrieveDocumentCategoriesAsync()
        {
            var documentCategories = await _documentDataService.RetrieveAllAsync<Entity.DocumentCategory>();
            return _mapper.Map<IEnumerable<DocumentCategory>>(documentCategories);
        }

        public async Task<ValidationResult<Document>> RetrieveDocumentByGuid(Guid documentGuid, string userId)
        {
            var validationResult = new ValidationResult<Document>();
            //  var workerId = await _workerBusinessService.RetrieveWorkerIdByUserId(userId);
            //  if (workerId == 0) return validationResult.Error("No worker found for logged in user");

            var document = await _documentDataService.RetrieveAsync<Entity.Document>(e => e.Guid == documentGuid);
            var result = _mapper.Map<IEnumerable<Document>>(document);
            return validationResult.Success(result.FirstOrDefault(), "");
        }

        public byte[] DownloadDocument(Guid documentGuid)
        {
            //var document = _documentServiceRestClient.Download(documentGuid);
            return null;
        }

        public async Task<ValidationResult<Document[]>> RetrieveDocumentsAsync(string userId, int? clientPersonnelId)
        {
            var validationResult = new ValidationResult<Document[]>();
            //var workerId = await _workerBusinessService.RetrieveWorkerIdByUserId(userId);
            //if (workerId == 0)
            //    return validationResult.Error("No worker found for logged in user");

            //var documentPredicate = PredicateBuilder.New<Entity.Document>(e => e. == workerId && e.GenericDocument);
            //documentPredicate = documentPredicate.And(e => e.ClientPersonnelId == clientPersonnelId);

            var documents = await _documentDataService.RetrieveAsync<Entity.Document>(e => e.PersonnelId == clientPersonnelId.Value.ToString());

            var result = _mapper.MapToList<Document>(documents).ToArray();

            return validationResult.Success(result, string.Empty);
        }

        public async Task<ValidationResult<Document[]>> RetrieveDocumentsByCategoryAsync(string userId, int documentCategoryId)
        {
            var validationResult = new ValidationResult<Document[]>();
            //   var workerId = await _workerBusinessService.RetrieveWorkerIdByUserId(userId);
            //  if (workerId == 0) return validationResult.Error("No worker found for logged in user");

            //var documentCategory = await RetrieveDocumentCategoryAsync(documentCategoryId);
            //if (documentCategory == null) return validationResult.Error("Invalid Document Category");

            var documents = await _documentDataService.RetrieveAsync<Entity.Document>(e => e.DocumentTypeId == documentCategoryId);
            var result = _mapper.MapToList<Document>(documents).ToArray();
            return validationResult.Success(result, string.Empty);
        }

        public async Task<ValidationResult<Document>> CreatePersonnelDocument(Document documentMeta, int personnelId, string userId)
        {

            // TODO Ensure Logged in User has access to this Worker resource at the controller level
            // We may be creating a document as admin on behalf of <userId>

            return await CreateWorkerDocument(documentMeta, personnelId, null, userId);
        }

        private async Task<ValidationResult<Document>> CreateWorkerDocument(Document documentMeta, int personnelId, List<int> clientPersonnelIds, string userId)
        {
            var validationResult = new ValidationResult<Document>();

            //upload document to document service
            var documentCategoryId = documentMeta.DocumentCategoryId;
            var documentCategories = await this.RetrieveDocumentCategoriesAsync();
            var documentCategory = documentCategories.FirstOrDefault(e => e.DocumentCategoryId == documentCategoryId);
            if (documentCategory == null) return validationResult.Error("Document category not found");

            var apiDocument = _mapper.Map<Entity.Document>(documentMeta);
            apiDocument.Product = "Egharpay";
            apiDocument.PersonnelId = personnelId.ToString();
            apiDocument.CreatedBy = userId;
            apiDocument.CreatedDateTime = DateTime.UtcNow;

            var documentGuid = CreateDocument(documentCategory, personnelId.ToString(), "Test", documentMeta.Description, documentMeta.Filename, documentMeta.Bytes);

            if (documentGuid == null)
                return validationResult.Error("Document could not be saved, please try again");

            // create a worker document - if we have clientPersonnelIds, we need a worker document for each one
            var personnelDocument = new Entity.Document()
            {
                DocumentTypeId = documentCategoryId.Value,
                Guid = documentGuid,
                PersonnelId = personnelId.ToString(),
                FileName = documentMeta.Filename
            };


            return validationResult;
        }

        //public async Task<ValidationResult<DocumentMeta>> CreateDocument(DocumentMeta documentMeta, DocumentServiceProduct product)
        //{
        //    var validationResult = new ValidationResult<DocumentMeta>();
        //    var documentCategoryId = documentMeta.DocumentTypeId;
        //    var documentCategory = await this.RetrieveDocumentCategoryAsync(documentCategoryId);
        //    if (documentCategory == null) return validationResult.Error("Document category not found");

        //    var document = Create(documentMeta, product.ToString(), documentCategory.Name, documentMeta.PayrollId);

        //    if (document == null) return validationResult.Error("Document could not be saved, please try again");
        //    var apiDocument = _mapper.Map<DocumentMeta>(document);
        //    return validationResult.Success(apiDocument, "Document Created");
        //}


        public Guid CreateDocument(DocumentCategory documentCategory, string personnelId, string userName, string description, string fileName, byte[] contents)
        {
            var newGuid = Guid.NewGuid();
            var basePath = CreateCentreBase(documentCategory.BasePath, "Egharpay");
            var categoryFileName = string.Concat(documentCategory.Name, "_", newGuid, "_", fileName);
            var employeeDirectory = GetPersonnelDirectory(basePath, personnelId) ?? CreateStudentDirectory(basePath, userName, personnelId);
            var categoryDirectory = Path.Combine(employeeDirectory, documentCategory.Name);
            var filePath = Path.Combine(categoryDirectory, categoryFileName);
            Directory.CreateDirectory(categoryDirectory);
            File.WriteAllBytes(filePath, contents);
            return newGuid;
        }

        private static string CreateCentreBase(string basepath, string centreName)
        {
            if (!Directory.Exists(Path.Combine(basepath, centreName)))
                Directory.CreateDirectory(Path.Combine(basepath, centreName));
            return Path.Combine(basepath, centreName);
        }

        private static string CreateStudentDirectory(string basePath, string studentName, string studentCode)
        {
            var directoryName = Path.Combine(basePath, CleanFilename(String.Format("{0}_{1}", studentName, studentCode)));
            Directory.CreateDirectory(directoryName);
            return directoryName;
        }

        private static string CleanFilename(string filename)
        {
            return Path.GetInvalidFileNameChars().Aggregate(filename, (current, chr) => current.Replace(chr, '_')).Replace(' ', '_');
        }

        private static string GetPersonnelDirectory(string basePath, string studentCode)
        {

            var employeeDirectories = Directory.GetDirectories(basePath, String.Format("*_{0}", studentCode));
            if (!employeeDirectories.Any())
                return null;
            if (employeeDirectories.Count() > 1)
                throw new Exception("Unable to identify employee");
            return employeeDirectories[0];
        }

        private static string CreateEmployeeDirectory(string basePath, string employeeName, string payrollId)
        {
            var directoryName = Path.Combine(basePath, CleanFilename(String.Format("{0}_{1}", employeeName, payrollId)));
            Directory.CreateDirectory(directoryName);
            return directoryName;
        }


    }

}
