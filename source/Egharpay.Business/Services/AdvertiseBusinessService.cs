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
    public partial class AdvertiseBusinessService : IAdvertiseBusinessService
    {
        protected IAdvertiseDataService _dataService;

        public AdvertiseBusinessService(IAdvertiseDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<Advertise> RetrieveAdvertise(int advertiseId)
        {
            var advertise = await _dataService.RetrieveAsync<Advertise>(a => a.AdvertiseId == advertiseId);
            return advertise.FirstOrDefault();
        }

        public async Task<PagedResult<Advertise>> RetrieveAdvertises(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var advertises = await _dataService.RetrievePagedResultAsync<Advertise>(a => true, orderBy, paging);
            return advertises;
        }
    }
}
