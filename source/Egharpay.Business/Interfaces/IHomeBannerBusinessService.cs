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
        Task<ValidationResult<Document>> CreateHomeBannerImage(Document document,int homeBannerId);
        Task<ValidationResult<HomeBannerDocumentDetail>> CreateHomeBannerDocumentDetail(HomeBannerDocumentDetail homeBannerDocumentDetail);

        //Retrieve
        Task<ValidationResult<HomeBanner>> RetrieveHomeBanner(int homeBannerId);
        Task<PagedResult<HomeBannerGrid>> RetrieveHomeBanners(List<OrderBy> orderBy = null, Paging paging = null);
        Task<List<HomeBannerImage>> RetrieveHomeBannerImages(DateTime startDateTime, DateTime endDateTime, string pincode);
        //Task<PagedResult<TrendComment>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);

        //Update
        Task<ValidationResult<HomeBanner>> UpdateHomeBanner(HomeBanner homeBanner);
    }
}
