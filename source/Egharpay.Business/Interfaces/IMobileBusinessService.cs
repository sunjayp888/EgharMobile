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
    public interface IMobileBusinessService
    {
        //Create
        Task<ValidationResult<Mobile>> CreateMobile(Mobile mobile);

        //Retrieve
        Task<Mobile> RetrieveMobile(int mobileId);
        Task<PagedResult<MobileGrid>> RetrieveMobiles(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<MobileGrid>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);
    }
}
