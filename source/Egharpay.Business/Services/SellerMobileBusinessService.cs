using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class SellerMobileBusinessService : ISellerMobileBusinessService
    {
        private readonly ISellerDataService _dataService;
        private readonly IMobileDataService _mobileDataService;
        private readonly IMapper _mapper;

        public SellerMobileBusinessService(ISellerDataService dataService, IMapper mapper, IMobileDataService mobileDataService)
        {
            _dataService = dataService;
            _mapper = mapper;
            _mobileDataService = mobileDataService;
        }

        public async Task<ValidationResult<SellerMobile>> AddMobileInStore(SellerMobile sellerMobile)
        {
            var validationResult = await MobileAlreadyAssign(sellerMobile.MobileId, sellerMobile.SellerId);
            if (!validationResult.Succeeded)
            {
                _mobileDataService.DeleteById<SellerMobile>(validationResult.Entity.SellerMobileId);
            }
            try
            {
                var mobiledata = await _mobileDataService.RetrieveAsync<Entity.Mobile>(e => e.MobileId == sellerMobile.MobileId);
                var mobile = mobiledata.FirstOrDefault();
                if (mobile != null)
                {
                    mobile.IsDeviceInStore = true;
                    await _mobileDataService.UpdateAsync(mobile);
                }
                await _dataService.CreateAsync(sellerMobile);
                validationResult.Entity = sellerMobile;
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

        public async Task<PagedResult<SellerMobileGrid>> RetrieveSellerMobileGrids(Expression<Func<SellerMobileGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var sellerMobiles = await _dataService.RetrievePagedResultAsync<SellerMobileGrid>(predicate, orderBy, paging);
            return sellerMobiles;
        }

        public async Task<PagedResult<SellerMobileGrid>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return await _dataService.RetrievePagedResultAsync<SellerMobileGrid>(a => a.SearchField.ToLower().Contains(term.ToLower()), orderBy, paging);
        }

        private async Task<ValidationResult<SellerMobile>> MobileAlreadyAssign(int mobileId, int sellerId)
        {
            var sellerMobile = await _dataService.RetrieveAsync<SellerMobile>(a => a.MobileId == mobileId && a.SellerId == sellerId);
            var alreadyExists = sellerMobile.Any();
            return new ValidationResult<SellerMobile>
            {
                Succeeded = !alreadyExists,
                Entity = sellerMobile.Any() ? sellerMobile.FirstOrDefault() : null,
                Errors = alreadyExists ? new List<string> { "Mobile already exists in store." } : null
            };
        }

    }
}
