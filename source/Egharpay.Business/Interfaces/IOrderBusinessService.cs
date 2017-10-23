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
    public interface IOrderBusinessService
    {
        //Create
        Task<ValidationResult<Order>> CreateOrder(Order order);

        //Retrieve
        Task<Order> RetrieveOrder(int orderId);
        Task<PagedResult<Order>> RetrieveOrders(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<Order>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);
    }
}
