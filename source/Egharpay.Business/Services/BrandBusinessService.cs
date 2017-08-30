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
   public partial class BrandBusinessService : IBrandBusinessService
    {
        protected IBrandDataService _dataService;

        public BrandBusinessService(IBrandDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<Brand> RetrieveBrand(int brandId)
        {
            var brand = await _dataService.RetrieveAsync<Brand>(a => a.BrandId == brandId);
            return brand.FirstOrDefault();
        }

        public async Task<PagedResult<BrandGrid>> RetrieveBrands(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var brands = await _dataService.RetrievePagedResultAsync<BrandGrid>(a => true, orderBy, paging);
            return brands;
        }

        public async Task<PagedResult<BrandGrid>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return await _dataService.RetrievePagedResultAsync<BrandGrid>(a => a.SearchField.ToLower().Contains(term.ToLower()), orderBy, paging);
        }
    }
}
