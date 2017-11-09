using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Mobile = Egharpay.Business.Models.Mobile;

namespace Egharpay.Business.Interfaces
{
    public interface IMobileBusinessService
    {
        //Create
        Task<ValidationResult<Models.Mobile>> CreateMobile(Models.Mobile mobile);
        Task<bool> CreateMobile(List<Models.Mobile> mobile);
        Task<bool> CreateBrand(List<Brand> brands);
        Task<bool> CreateMobileImage(List<MobileImage> brands);


        //Retrieve
        Task<Models.Mobile> RetrieveMobile(int mobileId);
        Task<List<MobileImage>> RetrieveMobileGalleryImages(int mobileId);
        Task<PagedResult<Models.Mobile>> RetrieveMobiles(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<Mobile>> Search(string term = null, List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<MobileGrid>> RetrieveMobilesByBrandId(int brandId,List<OrderBy> orderBy = null, Paging paging = null);
        Task<IEnumerable<Models.Mobile>> RetrieveLatestMobile();
        Task<IEnumerable<MetaSearchKeyword>> RetrieveMetaSearchKeyword();
    }
}
