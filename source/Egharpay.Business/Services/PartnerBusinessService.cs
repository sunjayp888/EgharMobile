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
    public partial class PartnerBusinessService : IPartnerBusinessService
    {
        protected IPartnerDataService _dataService;
        protected IMapper _mapper;

        public PartnerBusinessService(IPartnerDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
        public async Task<ValidationResult<Partner>> CreatePartner(Partner partner)
        {
            ValidationResult<Partner> validationResult = new ValidationResult<Partner>();
            try
            {
                var mobileData = _mapper.Map<Partner>(partner);
                await _dataService.CreateAsync(mobileData);
                validationResult.Entity = partner;
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

        public async Task<Partner> RetrievePartner(int partnerId)
        {
            var partner = await _dataService.RetrieveAsync<Partner>(a => a.PartnerId == partnerId);
            return partner.FirstOrDefault();
        }

        public async Task<PagedResult<Partner>> RetrievePartners(Expression<Func<Partner, bool>> expression, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var partners = await _dataService.RetrievePagedResultAsync<Partner>(expression, orderBy, paging);
            return partners;
        }

        public async Task<ValidationResult<Partner>> UpdatePartner(Partner partner)
        {
            ValidationResult<Partner> validationResult = new ValidationResult<Partner>();
            try
            {
                await _dataService.UpdateAsync(partner);
                validationResult.Entity = partner;
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
