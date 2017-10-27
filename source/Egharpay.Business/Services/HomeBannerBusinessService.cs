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
        protected IDocumentsBusinessService _documentsBusinessService;

        public HomeBannerBusinessService(IHomeBannerDataService dataService, IDocumentsBusinessService documentsBusinessService)
        {
            _dataService = dataService;
            _documentsBusinessService = documentsBusinessService;
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

        public async Task<ValidationResult<Document>> CreateHomeBannerImage(Document document, int homeBannerId)
        {
            ValidationResult<Document> validationResult = new ValidationResult<Document>();
            try
            {
                var result = await _documentsBusinessService.CreateDocument(document);
                if (result.Succeeded)
                {
                    var homeBannerDocument = new HomeBannerDocumentDetail()
                    {
                        HomeBannerId = homeBannerId,
                        DocumentDetailId = result.Entity.ProductId // Just Confirm what should pass to documentdetailid
                    };
                    await _dataService.CreateAsync(homeBannerDocument);
                }
                validationResult.Entity = document;
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

        public async Task<ValidationResult<HomeBannerDocumentDetail>> CreateHomeBannerDocumentDetail(HomeBannerDocumentDetail homeBannerDocumentDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult<HomeBanner>> RetrieveHomeBanner(int homeBannerId)
        {
            var homeBanner = await _dataService.RetrieveByIdAsync<HomeBanner>(homeBannerId);
            if (homeBanner != null)
            {
                var validationResult = new ValidationResult<HomeBanner>
                {
                    Entity = homeBanner,
                    Succeeded = true
                };
                return validationResult;
            }
            return new ValidationResult<HomeBanner>
            {
                Succeeded = false,
                Errors = new[] { string.Format("No Home Banner found with Id: {0}", homeBannerId) }
            };
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
