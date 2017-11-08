using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        protected IMapper _mapper;

        public MobileBusinessService(IMobileDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        public async Task<ValidationResult<Models.Mobile>> CreateMobile(Models.Mobile mobile)
        {
            ValidationResult<Models.Mobile> validationResult = new ValidationResult<Models.Mobile>();
            try
            {
                var mobileData = _mapper.Map<Entity.Mobile>(mobile);
                await _dataService.CreateAsync(mobileData);
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

        public async Task<bool> CreateMobile(List<Models.Mobile> mobile)
        {
            try
            {
                var mobileData = _mapper.MapToList<Entity.Mobile>(mobile);
                await _dataService.CreateRangeAsync(mobileData);
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

        public async Task<Models.Mobile> RetrieveMobile(int mobileId)
        {
            var result = await _dataService.RetrieveAsync<Entity.Mobile>(a => a.MobileId == mobileId);
            var mobile = _mapper.MapToList<Models.Mobile>(result);
            return mobile.FirstOrDefault();
        }

        public async Task<List<MobileImage>> RetrieveMobileGalleryImages(int mobileId)
        {
            var category = await _dataService.RetrieveAsync<Entity.DocumentCategory>(e => e.Name.ToLower() == "mobilegalleryimage");
            var basePath = "Need to change";
            var mobile = await _dataService.RetrieveByIdAsync<Entity.Mobile>(mobileId);
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

        public async Task<PagedResult<Models.Mobile>> RetrieveMobiles(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var result = await _dataService.RetrievePagedResultAsync<Entity.Mobile>(a => true, orderBy, paging);
            return _mapper.MapToPagedResult<Models.Mobile>(result);
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

        public async Task<IEnumerable<Models.Mobile>> RetrieveLatestMobile()
        {
            var result = await _dataService.RetrieveAsync<Entity.Mobile>(e => e.IsLatest);
            return _mapper.MapToList<Models.Mobile>(result);
        }
    }
}
