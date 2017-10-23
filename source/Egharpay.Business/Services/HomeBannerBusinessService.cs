using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
    public partial class HomeBannerBusinessService : IHomeBannerBusinessService
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

        public async Task<List<HomeBannerImage>> RetrieveHomeBannerImages(DateTime startDateTime, DateTime endDateTime, string pincode)
        {
            var category = await _dataService.RetrieveAsync<Entity.DocumentCategory>(e => e.Name.ToLower() == "homebanner");
            var basePath = "Need Changes";
            var homeBanners = await _dataService.RetrievePagedResultAsync<HomeBanner>(e => e.StartDateTime == startDateTime && e.EndDateTime == endDateTime && e.Pincode == pincode);
            var homeBannerList = homeBanners.Items.ToList();
            var homeBannerImageList = new List<HomeBannerImage>();
            if (!string.IsNullOrEmpty(basePath))
            {
                    homeBannerImageList.AddRange(homeBannerList.Select(item => new HomeBannerImage()
                    {
                        ImagePath = Path.Combine(basePath, item.ImagePath)
                }));
                
                return homeBannerImageList;
            }
            return homeBannerImageList;
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
