using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IBrandBusinessService
    {
        //Retrieve
        Task<Brand> RetrieveBrand(int brandId);
        Task<PagedResult<BrandGrid>> RetrieveBrands(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<BrandGrid>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);
    }
}
