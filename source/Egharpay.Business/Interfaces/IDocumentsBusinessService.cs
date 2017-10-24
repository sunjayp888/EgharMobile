using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using DocumentCategory = Egharpay.Business.Models.DocumentCategory;


namespace Egharpay.Business.Interfaces
{
    public interface IDocumentsBusinessService
    {
        #region Create
        Task<ValidationResult<Document>> CreateDocument(Document document);
        byte[] DownloadDocument(Guid documentGuid);
        #endregion

        #region Retrieve
        Task<IEnumerable<DocumentCategory>> RetrieveDocumentCategoriesAsync();
        Task<ValidationResult<Document>> RetrieveDocumentByGuid(Guid documentGuid, string userId);
        Task<ValidationResult<Document[]>> RetrieveDocuments(int personnelId, Enum.DocumentCategory category);
        #endregion

        #region Delete
        Task<bool> DeleteDocument(List<Guid> guid);
        #endregion
    }
}