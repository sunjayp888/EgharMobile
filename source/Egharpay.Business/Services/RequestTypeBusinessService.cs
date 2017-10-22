using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Services
{
    public partial class RequestTypeBusinessService :IRequestTypeBusinessService
    {
        protected IRequestTypeDataService _dataService;

        public RequestTypeBusinessService(IRequestTypeDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<RequestType> RetrieveRequestType(int requestTypeId)
        {
            var requestType = await _dataService.RetrieveAsync<RequestType>(a => a.RequestTypeId == requestTypeId);
            return requestType.FirstOrDefault();
        }

        public async Task<PagedResult<RequestType>> RetrieveRequestTypes(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var requestTypes = await _dataService.RetrievePagedResultAsync<RequestType>(a => true, orderBy, paging);
            return requestTypes;
        }
    }
}
