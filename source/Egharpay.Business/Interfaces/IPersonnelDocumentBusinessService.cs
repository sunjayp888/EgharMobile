using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;

namespace Egharpay.Business.Interfaces
{
    public interface IPersonnelDocumentBusinessService
    {
        Task<ValidationResult<Document[]>> RetrievePersonnelDocuments(int personnelId, Enum.DocumentCategory category);
        Task<ValidationResult<Document>> RetrievePersonnelProfileImage(int personnelId);

    }
}
