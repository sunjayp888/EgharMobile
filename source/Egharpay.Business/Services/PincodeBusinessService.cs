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
    public partial class PincodeBusinessService : IPincodeBusinessService
    {
        protected IPincodeDataService _dataService;

        public PincodeBusinessService(IPincodeDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<PagedResult<PincodeDataGrid>> RetrievePincodes(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var pincode = await _dataService.RetrievePagedResultAsync<PincodeDataGrid>(a => true, orderBy, paging);
            return pincode;
        }

        public async Task<PagedResult<PincodeDataGrid>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return await _dataService.RetrievePagedResultAsync<PincodeDataGrid>(a => a.SearchField.ToLower().Contains(term.ToLower()), orderBy, paging);
        }
    }
}
