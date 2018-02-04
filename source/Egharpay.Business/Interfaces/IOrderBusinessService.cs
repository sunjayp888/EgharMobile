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
    public interface IOrderBusinessService
    {
        //Create
        Task<ValidationResult<Order>> CreateOrder(int mobileId, int personnelId, List<int> sellerIds);
        Task<ValidationResult<OrderSeller>> CreateOrderSeller(OrderSeller orderSeller);

        //Retrieve
        Task<Order> RetrieveOrder(int orderId);
        Task<PagedResult<Order>> RetrieveOrders(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<Order>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);

        Task<PagedResult<SellerOrderGrid>> RetrieveSellerOrders(Expression<Func<SellerOrderGrid, bool>> expression, List<OrderBy> orderBy = null, Paging paging = null);

        //Update
        Task<ValidationResult<Order>> UpdateOrder(Order order);
    }
}
