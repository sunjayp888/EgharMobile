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
    public interface IMobileBusinessService
    {
        //Create
        Task<ValidationResult<Mobile>> CreateMobile(Mobile mobile);
        Task<bool> CreateMobile(List<Mobile> mobile);
        Task<bool> CreateBrand(List<Brand> brands);
        Task<bool> CreateMobileImage(List<MobileImage> brands);


        //Retrieve
        Task<Mobile> RetrieveMobile(int mobileId);
        Task<List<MobileImage>> RetrieveMobileGalleryImages(int mobileId);
        Task<PagedResult<Mobile>> RetrieveMobiles(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<MobileGrid>> Search(string term = null, List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<MobileGrid>> RetrieveMobilesByBrandId(int brandId,List<OrderBy> orderBy = null, Paging paging = null);
    }
}
