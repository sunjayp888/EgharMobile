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
        IEnumerable<DocumentCategory> RetrieveDocumentTypes();
        PagedResult<Document> RetrieveDocuments(Expression<Func<Document, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
    }
}
