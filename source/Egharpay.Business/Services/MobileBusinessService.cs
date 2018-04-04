using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Egharpay.Business.Dto;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Extensions;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using LinqKit;
using Mobile = Egharpay.Business.Models.Mobile;

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
            //For version 1.1 we are not taking from documentdetail table but need to implement in future.
            var basePath = ConfigHelper.TemporaryMobileGalleryImagePath;
            var result = await _dataService.RetrieveAsync<Entity.Mobile>(m => m.MobileId == mobileId, null, e => e.Brand);
            var mobileImageList = new List<MobileImage>();
            var mobile = result.FirstOrDefault();
            if (!string.IsNullOrEmpty(basePath) && mobile != null)
            {
                var mobilePath = Path.Combine(basePath, mobile.Brand.Name, mobile.Name);
                var fileNames = Directory.GetFiles(mobilePath).ToList();
                mobileImageList.AddRange(fileNames.Select(item => new MobileImage
                {
                    ImagePath = string.Format("/MobileImage/{0}/{1}/{2}", mobile.Brand.Name, mobile.Name, item.Split('\\').LastOrDefault())
                }));
                return mobileImageList;
            }
            return mobileImageList;
        }

        public async Task<PagedResult<Mobile>> RetrieveMobiles(Expression<Func<Entity.MobileGrid, bool>> expression, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var result = await _dataService.RetrievePagedResultAsync<MobileGrid>(expression, orderBy, paging);
            return _mapper.MapToPagedResult<Models.Mobile>(result);
        }

        public async Task<PagedResult<Mobile>> RetrieveMobiles(Filter filter, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var predicate = BuildMobileSearchPredicate(filter);
            var result = await _dataService.RetrievePagedResultAsync<Entity.Mobile>(predicate, orderBy, paging);
            return _mapper.MapToPagedResult<Models.Mobile>(result);
        }

        public async Task<PagedResult<Mobile>> Search(string term = null, List<OrderBy> orderBy = null, Paging paging = null)
        {
            if (!string.IsNullOrEmpty(term))
            {
                try
                {
                    var data = _dataService.Search(term.Replace(" ", "").ToLower(), orderBy, paging);
                    var dataPage = await data.PaginateAsync(paging);
                    return _mapper.MapToPagedResult<Mobile>(dataPage);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            var result = await _dataService.RetrievePagedResultAsync<MobileGrid>(e => true);
            return _mapper.MapToPagedResult<Mobile>(result);
        }

        public async Task<PagedResult<MobileGrid>> RetrieveMobilesByBrandId(int brandId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var mobiles = await _dataService.RetrievePagedResultAsync<MobileGrid>(a => a.BrandId == brandId, orderBy, paging);
            return mobiles;
        }

        public async Task<IEnumerable<Mobile>> RetrieveLatestMobile()
        {
            var result = await _dataService.RetrieveAsync<Entity.Mobile>(e => e.IsLatest);
            return _mapper.MapToList<Models.Mobile>(result);
        }

        public async Task<IEnumerable<MetaSearchKeyword>> RetrieveMetaSearchKeyword()
        {
            return await _dataService.RetrieveAsync<MetaSearchKeyword>(e => true);
        }

        private ExpressionStarter<Entity.Mobile> BuildMobileSearchPredicate(Filter filter)
        {
            var predicate = PredicateBuilder.New<Entity.Mobile>(true);
            if (filter != null && filter.IsPriceFilter)
                predicate = predicate.And(e => e.Price >= filter.FromPrice && e.Price <= filter.ToPrice);

            if (filter != null && filter.IsBrandFilter)
                predicate = predicate.And(e => e.BrandId == filter.BrandId);

            if (filter != null && filter.IsPrimaryCameraFilter)
                predicate = predicate.And(e => e.PrimaryCamera >= filter.FromPrimaryCameraSize && e.PrimaryCamera <= filter.FromPrimaryCameraSize);

            if (filter != null && filter.IsSecondaryCameraFilter)
                predicate = predicate.And(e => e.SecondaryCamera >= filter.FromSecondaryCameraSize && e.SecondaryCamera <= filter.ToSecondaryCameraSize);

            if (filter != null && filter.IsRamSizeFilter)
                predicate = predicate.And(e => e.RAM >= filter.FromRamSize && e.RAM <= filter.ToRamSize);

            if (filter != null && filter.IsDeviceInStore)
                predicate = predicate.And(e => e.IsDeviceInStore);

            return predicate;
        }
    }
}
