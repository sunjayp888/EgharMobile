using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Services
{
    public partial class HomeBannerBusinessService:IHomeBannerBusinessService
    {
        protected IHomeBannerDataService _dataService;

        public HomeBannerBusinessService(IHomeBannerDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<ValidationResult<HomeBanner>> CreateHomeBanner(HomeBanner homeBanner)
        {
            ValidationResult<HomeBanner> validationResult = new ValidationResult<HomeBanner>();
            try
            {
                await _dataService.CreateAsync(homeBanner);
                validationResult.Entity = homeBanner;
                validationResult.Succeeded = true;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        public async Task<HomeBanner> RetrieveHomeBanner(int homeBannerId)
        {
            var homeBanner = await _dataService.RetrieveAsync<HomeBanner>(a => a.HomeBannerId == homeBannerId);
            return homeBanner.FirstOrDefault();
        }

        public async Task<PagedResult<HomeBannerGrid>> RetrieveHomeBanners(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var homeBanners = await _dataService.RetrievePagedResultAsync<HomeBannerGrid>(a => true, orderBy, paging);
            return homeBanners;
        }

        public async Task<ValidationResult<HomeBanner>> UpdateHomeBanner(HomeBanner homeBanner)
        {
            ValidationResult<HomeBanner> validationResult = new ValidationResult<HomeBanner>();
            try
            {
                await _dataService.UpdateAsync(homeBanner);
                validationResult.Entity = homeBanner;
                validationResult.Succeeded = true;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }
    }
}
