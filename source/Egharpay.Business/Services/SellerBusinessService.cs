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
    public partial class SellerBusinessService : ISellerBusinessService
    {
        protected ISellerDataService _dataService;

        public SellerBusinessService(ISellerDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<ValidationResult<Seller>> CreateSeller(Seller seller)
        {
            ValidationResult<Seller> validationResult = new ValidationResult<Seller>();
            try
            {
                await _dataService.CreateAsync(seller);
                validationResult.Entity = seller;
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

        public async Task<Seller> RetrieveSeller(int sellerId)
        {
            var seller = await _dataService.RetrieveAsync<Seller>(a => a.SellerId == sellerId);
            return seller.FirstOrDefault();
        }

        public async Task<PagedResult<Seller>> RetrieveSellers(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var sellers = await _dataService.RetrievePagedResultAsync<Seller>(a => true, orderBy, paging);
            return sellers;
        }

        public async Task<PagedResult<SellerGrid>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return await _dataService.RetrievePagedResultAsync<SellerGrid>(a => a.SearchField.ToLower().Contains(term.ToLower()), orderBy, paging);
        }
    }
}
