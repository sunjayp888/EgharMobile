using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using DocumentCategory = Egharpay.Business.Models.DocumentCategory;


namespace Egharpay.Business.Interfaces
{
    public interface IDocumentsBusinessService
    {
        Task<ValidationResult<Document>> CreatePersonnelDocument(Document document, int personnelId, string userId);
        byte[] DownloadDocument(Guid documentGuid);
        Task<IEnumerable<DocumentCategory>> RetrieveDocumentCategoriesAsync();
        Task<ValidationResult<Document>> RetrieveDocumentByGuid(Guid documentGuid, string userId);
        Task<ValidationResult<Document[]>> RetrieveDocumentsAsync(string userId, int? personnelId);
        Task<ValidationResult<Document[]>> RetrieveDocumentsByCategoryAsync(string userId, int documentCategoryId);
    }
}