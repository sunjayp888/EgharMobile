using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IRequestTypeBusinessService
    {
        //Retrieve
        Task<RequestType> RetrieveRequestType(int requestTypeId);
        Task<PagedResult<RequestType>> RetrieveRequestTypes(List<OrderBy> orderBy = null, Paging paging = null);
    }
}
