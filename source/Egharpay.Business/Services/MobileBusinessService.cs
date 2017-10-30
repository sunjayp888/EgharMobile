using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class MobileBusinessService : IMobileBusinessService
    {
        protected IMobileDataService _dataService;

        public MobileBusinessService(IMobileDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<ValidationResult<Mobile>> CreateMobile(Mobile mobile)
        {
            ValidationResult<Mobile> validationResult = new ValidationResult<Mobile>();
            try
            {
                await _dataService.CreateAsync(mobile);
                validationResult.Entity = mobile;
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

        public async Task<bool> CreateMobile(List<Mobile> mobile)
        {
            try
            {
                await _dataService.CreateRangeAsync(mobile);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateBrand(List<Brand> brands)
        {
            try
            {
                await _dataService.CreateRangeAsync(brands);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateMobileImage(List<MobileImage> mobileImage)
        {
            try
            {
                await _dataService.CreateRangeAsync(mobileImage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Mobile> RetrieveMobile(int mobileId)
        {
            var mobile = await _dataService.RetrieveAsync<Mobile>(a => a.MobileId == mobileId);
            return mobile.FirstOrDefault();
        }

        public async Task<List<MobileImage>> RetrieveMobileGalleryImages(int mobileId)
        {
            var category = await _dataService.RetrieveAsync<Entity.DocumentCategory>(e => e.Name.ToLower() == "mobilegalleryimage");
            var basePath = "Need to change";
            var mobile = await _dataService.RetrieveByIdAsync<Mobile>(mobileId);
            var mobileImageList = new List<MobileImage>();
            if (!string.IsNullOrEmpty(basePath))
            {
                var mobilePath = Path.Combine(basePath, mobile.Brand.Name, mobile?.Name);
                try
                {
                    var fileNamest = Directory.GetFiles(mobilePath).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                var fileNames = Directory.GetFiles(mobilePath).ToList();
                mobileImageList.AddRange(fileNames.Select(item => new MobileImage
                {
                    ImagePath = item
                }));
                return mobileImageList;
            }
            return mobileImageList;
        }

        public async Task<PagedResult<Mobile>> RetrieveMobiles(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var mobiles = await _dataService.RetrievePagedResultAsync<Mobile>(a => true, orderBy, paging);
            return mobiles;
        }

        public async Task<PagedResult<MobileGrid>> Search(string term = null, List<OrderBy> orderBy = null, Paging paging = null)
        {
            if (!string.IsNullOrEmpty(term))
                return await _dataService.RetrievePagedResultAsync<MobileGrid>(a => a.SearchField.ToLower().Contains(term.ToLower()), orderBy, paging);
            return null;
        }

        public async Task<PagedResult<MobileGrid>> RetrieveMobilesByBrandId(int brandId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var mobiles = await _dataService.RetrievePagedResultAsync<MobileGrid>(a => a.BrandId == brandId, orderBy, paging);
            return mobiles;
        }

        public async Task<IEnumerable<Mobile>> RetrieveLatestMobile()
        {
            var mobileResult = await _dataService.RetrieveAllAsync<Mobile>(null, e => e.IsLatest.Value);
            return mobileResult;
        }
    }
}
