using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Services
{
    public partial class OrderBusinessService :IOrderBusinessService
    {
        protected IOrderDataService _dataService;
        protected IMapper _mapper;
        public OrderBusinessService(IOrderDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<ValidationResult<Order>> CreateOrder(Order order)
        {
            ValidationResult<Order> validationResult = new ValidationResult<Order>();
            try
            {
                await _dataService.CreateAsync(order);
                validationResult.Entity = order;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        public async Task<Order> RetrieveOrder(int orderId)
        {
            var order = await _dataService.RetrieveAsync<Order>(a => a.OrderId == orderId);
            return order.FirstOrDefault();
        }

        public async Task<PagedResult<Order>> RetrieveOrders(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var orders = await _dataService.RetrievePagedResultAsync<Order>(a => true, orderBy, paging);
            return orders;
        }

        public async Task<PagedResult<Order>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return null;
        }

        public async Task<PagedResult<SellerOrderGrid>> RetrieveSellerOrders(Expression<Func<SellerOrderGrid, bool>> expression, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var result = await _dataService.RetrievePagedResultAsync<SellerOrderGrid>(expression, orderBy, paging);
            return _mapper.MapToPagedResult<SellerOrderGrid>(result);
        }
    }
}
