using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IAdvertiseBusinessService
    {
        //Retrieve
        Task<Advertise> RetrieveAdvertise(int advertiseId);
        Task<PagedResult<Advertise>> RetrieveAdvertises(List<OrderBy> orderBy = null, Paging paging = null);
    }
}
