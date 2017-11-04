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
    public interface IHomeBannerBusinessService
    {
        //Create
        Task<ValidationResult<HomeBanner>> CreateHomeBanner(HomeBanner homeBanner);
        Task<ValidationResult<Document>> CreateHomeBannerImage(Document document, int homeBannerId);

        //Retrieve
        Task<ValidationResult<HomeBanner>> RetrieveHomeBanner(int homeBannerId);
        Task<PagedResult<HomeBanner>> RetrieveHomeBanners(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<HomeBannerImage>> RetrieveHomeBannerImages(Expression<Func<HomeBannerImage, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);

        //Update
        Task<ValidationResult<HomeBanner>> UpdateHomeBanner(HomeBanner homeBanner);
    }
}
