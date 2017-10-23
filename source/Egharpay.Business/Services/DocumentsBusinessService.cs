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
using Egharpay.Entity;
using DocumentCategory = Egharpay.Business.Models.DocumentCategory;

namespace Egharpay.Business.Services
{
    public class DocumentsBusinessService : IDocumentsBusinessService
    {
        private readonly IMapper _mapper;
        private readonly IDocumentDataService _documentDataService;
        private readonly IProductDataService _productDataService;
        private string Product => "Egharpay";

        public DocumentsBusinessService(IMapper mapper, IDocumentDataService documentDataService, IProductDataService productDataService)
        {
            _mapper = mapper;
            _documentDataService = documentDataService;
            _productDataService = productDataService;
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

            var document = await _documentDataService.RetrieveAsync<Entity.DocumentDetail>(e => e.DocumentGUID == documentGuid);
            var result = _mapper.Map<IEnumerable<Document>>(document);
            return validationResult.Success(result.FirstOrDefault(), "");
        }

        public byte[] DownloadDocument(Guid documentGuid)
        {
            //var document = _documentServiceRestClient.Download(documentGuid);
            return null;
        }

        public async Task<ValidationResult<Document[]>> RetrieveDocumentsAsync(string userId, int? personnelId)
        {
            var validationResult = new ValidationResult<Document[]>();
            var documents = await _documentDataService.RetrieveAsync<Entity.DocumentDetail>(e => e.PersonnelId == personnelId.ToString());
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

            var documents = await _documentDataService.RetrieveAsync<Entity.DocumentDetail>(e => e.CategoryId == documentCategoryId);
            var result = _mapper.MapToList<Document>(documents).ToArray();
            return validationResult.Success(result, string.Empty);
        }

        public async Task<ValidationResult<Document>> CreatePersonnelDocument(string documentCategory, int personnelId, string fullName, string userId, byte[] bytes)
        {
            var validationResult = new ValidationResult<Document>();
            //upload document to document service
            var documentCategories = await this.RetrieveDocumentCategoriesAsync();
            var category = documentCategories.FirstOrDefault(e => e.Name.ToLower() == documentCategory.ToLower());
            if (category == null) return validationResult.Error("Document category not found");
            var products = _productDataService.RetrieveAll<Product>();
            var productId = products.Single(p => p.Name.ToLower() == Product.ToLower()).ProductId;
            var documentData = new Document()
            {
                CategoryId = category.DocumentCategoryId,
                ProductId = productId,
                PersonnelName = fullName,
                CreatedDateUtc = DateTime.UtcNow,
                CreatedBy = userId,
                PersonnelId = personnelId.ToString(),
                Content = bytes,
            };

            var documentGuid = CreateDocument(documentData);

            if (documentGuid == null)
                return validationResult.Error("Document could not be saved, please try again");

            // create a worker document - if we have clientPersonnelIds, we need a worker document for each one
            try
            {
                var entity = await CreateDocument(documentData);
                validationResult.Entity = _mapper.Map<Document>(entity);
                validationResult.Succeeded = true;
            }
            catch (Exception)
            {
                validationResult.Succeeded = false;
            }
            return validationResult;
        }

        public async Task<DocumentDetail> CreateDocument(Document document)
        {
            var newGuid = Guid.NewGuid();
            // this categoryFileName ensures uniqueness of file in folder and is critical
            var categoryFileName = string.Format("{0}_{1}_{2}", document.Category, newGuid, document.FileName);
            var basePath = GetBasePath(document.Product);
            // sjp retain compatibility with existing DocumentService
            var employeeDirectory = CreatePersonnelDirectory(basePath, document.PersonnelName, document.PersonnelId);
            var categoryDirectory = Path.Combine(employeeDirectory, document.Category);
            var filePath = Path.Combine(categoryDirectory, categoryFileName);

            var documentDetail = new DocumentDetail
            {
                DocumentGUID = newGuid,
                ProductId = document.ProductId,
                CategoryId = document.CategoryId,
                PersonnelId = document.PersonnelId,
                Description = document.Description,
                FileName = document.FileName,
                ClientFileName = document.ClientFileName,
                CreatedDateUTC = DateTime.UtcNow,
                CreatedBy = document.CreatedBy,
                RequireApproval = document.RequireApproval,
                UncPath = basePath,
                RelativePath = filePath.Replace(basePath, string.Empty),
                DocumentBatchId = document.DocumentBatchId,
                DownloadedDateUtc = document.DownloadedDateUtc,
                EmailSentDateUtc = document.EmailSentDateUtc
            };

            Directory.CreateDirectory(categoryDirectory);

            if (document.Content != null)
            {
                File.WriteAllBytes(filePath, document.Content);
            }
            else if (!string.IsNullOrEmpty(document.ContentBase64))
            {
                byte[] content = Convert.FromBase64String(document.ContentBase64);
                File.WriteAllBytes(filePath, content);
            }
            documentDetail = await _documentDataService.CreateGetAsync(documentDetail);
            // update the DTO
            document.DocumentGuid = documentDetail.DocumentGUID;
            document.CreatedDateUtc = documentDetail.CreatedDateUTC;
            document.CreatedBy = documentDetail.CreatedBy;

            return documentDetail;
        }

        private string GetBasePath(string productName)
        {
            var products = _productDataService.RetrieveAll<Product>();
            var prod = products.FirstOrDefault(p => p.Name.ToLower() == productName.ToLower());
            if (prod == null)
                throw new Exception("Invalid product");
            return prod.UncPath;
        }

        private static string CleanFilename(string filename)
        {
            return Path.GetInvalidFileNameChars().Aggregate(filename, (current, chr) => current.Replace(chr, '_')).Replace(' ', '_');
        }

        private static string CreatePersonnelDirectory(string basePath, string employeeName, string payrollId)
        {
            var directoryName = Path.Combine(basePath, CleanFilename(String.Format("{0}_{1}", employeeName, payrollId)));
            Directory.CreateDirectory(directoryName);
            return directoryName;
        }

    }

}
