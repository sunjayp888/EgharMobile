using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface ISellerMobileBusinessService
    {
        Task<ValidationResult<SellerMobile>> AddMobileInStore(SellerMobile seller);

        //Retrieve
        Task<PagedResult<SellerMobileGrid>> RetrieveSellerMobileGrids(Expression<Func<SellerMobileGrid, bool>> predicate,List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<SellerMobileGrid>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);
    }
}
