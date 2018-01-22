using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.EmailServiceReference;
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
        private readonly ISellerDataService _dataService;
        private readonly IEmailBusinessService _emailBusinessService;

        public SellerBusinessService(ISellerDataService dataService, IEmailBusinessService emailBusinessService)
        {
            _dataService = dataService;
            _emailBusinessService = emailBusinessService;
        }

        public async Task<ValidationResult<Seller>> CreateSeller(Seller seller)
        {
            ValidationResult<Seller> validationResult = new ValidationResult<Seller>();
            try
            {
                await _dataService.CreateAsync(seller);
                //Send Email
                SendSellerEmail(seller);
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

        private void SendSellerEmail(Seller seller)
        {
            var emailData = new EmailData()
            {
                BCCAddressList = new List<string> { "sunjayp88@gmail.com" },
                Body = String.Format("Dear {0} , Thanks For Registering on Mumbile.Com", seller.Owner),
                Subject = "Welcome To Mumbile.Com",
                IsHtml = true,
                ToAddressList = new List<string> { seller.Email.ToLower() }
            };
            _emailBusinessService.SendEmail(emailData);
        }

        public async Task<Seller> RetrieveSeller(int sellerId)
        {
            var seller = await _dataService.RetrieveAsync<Seller>(a => a.SellerId == sellerId);
            return seller.FirstOrDefault();
        }

        public async Task<Seller> RetrieveSellerByPersonnelId(int personnelId)
        {
            var seller = await _dataService.RetrieveAsync<Seller>(a => a.PersonnelId == personnelId);
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

        public async Task<List<Seller>> RetrieveSellers(List<int> sellerIds)
        {
            var sellers = await _dataService.RetrieveAsync<Seller>(s => sellerIds.Contains(s.SellerId));
            return sellers.ToList();
        }

        public async Task<ValidationResult<Seller>> UpdateSeller(Seller seller)
        {
            ValidationResult<Seller> validationResult = new ValidationResult<Seller>();
            try
            {
                await _dataService.UpdateAsync(seller);
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
    }
}
