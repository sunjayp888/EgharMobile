using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IPincodeBusinessService
    {
        //Retrieve
        Task<PagedResult<PincodeDataGrid>> RetrievePincodes(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<PincodeDataGrid>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);
    }
}
