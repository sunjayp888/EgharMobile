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
    public interface ISellerOrderBusinessService
    {
        Task<PagedResult<SellerOrder>> RetrieveSellerOrders(Expression<Func<SellerOrderGrid, bool>> expression, List<OrderBy> orderBy = null, Paging paging = null);
    }
}
