using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IPersonnelDocumentBusinessService
    {
        Task<ValidationResult<Document[]>> RetrievePersonnelDocuments(int personnelId, Enum.DocumentCategory category);
        Task<ValidationResult<Document>> RetrievePersonnelProfileImage(int personnelId);
        Task<PagedResult<Document>> RetrievePersonnelDocuments(int personnelId, Paging paging = null, List<OrderBy> orderBy = null);
        Task<PagedResult<PersonnelDocumentDetail>> RetrievePersonnelSelfies(DateTime startDateTime, DateTime endDateTime);

    }
}
