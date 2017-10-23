using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IDocumentBusinessService
    {
        #region Create
        Guid Create(int categoryId, int personnelId, string personnelName, string description, string fileName, byte[] contents); 
        #endregion

        #region Retrieve
        IEnumerable<DocumentCategory> RetrieveDocumentTypes();
        PagedResult<DocumentDetail> RetrieveDocuments(Expression<Func<DocumentDetail, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        byte[] GetDocumentBytes(string path);
        List<DocumentDetail> RetrieveDocuments(string category, int personnelId); 
        #endregion
    }
}
