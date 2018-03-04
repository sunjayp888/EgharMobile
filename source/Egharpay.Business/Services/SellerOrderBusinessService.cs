using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Egharpay.Business.Interfaces;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Services
{
    public partial class SellerOrderBusinessService : ISellerOrderBusinessService
    {
        protected ISellerOrderDataService _dataService;
        protected IMapper _mapper;

        public SellerOrderBusinessService(ISellerOrderDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        public async Task<PagedResult<SellerOrder>> RetrieveSellerOrders(Expression<Func<SellerOrderGrid, bool>> expression, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var result = await _dataService.RetrievePagedResultAsync<SellerOrderGrid>(expression, orderBy, paging);
            return _mapper.MapToPagedResult<SellerOrder>(result);
        }
    }
}
