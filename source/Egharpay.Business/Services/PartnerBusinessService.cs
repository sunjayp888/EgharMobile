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
        public async Task<ValidationResult<PartnerEnquiry>> CreatePartner(PartnerEnquiry partnerEnquiry)
        {
            ValidationResult<PartnerEnquiry> validationResult = new ValidationResult<PartnerEnquiry>();
            try
            {
                partnerEnquiry.CreatedDate = DateTime.UtcNow;
                var mobileData = _mapper.Map<PartnerEnquiry>(partnerEnquiry);
                await _dataService.CreateAsync(mobileData);
                validationResult.Entity = partnerEnquiry;
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

        public async Task<PartnerEnquiry> RetrievePartner(int partnerId)
        {
            var partner = await _dataService.RetrieveAsync<PartnerEnquiry>(a => a.PartnerEnquiryId == partnerId);
            return partner.FirstOrDefault();
        }

        public async Task<PagedResult<PartnerEnquiry>> RetrievePartners(Expression<Func<PartnerEnquiry, bool>> expression, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var partners = await _dataService.RetrievePagedResultAsync<PartnerEnquiry>(expression, orderBy, paging);
            return partners;
        }

        public async Task<ValidationResult<PartnerEnquiry>> UpdatePartner(PartnerEnquiry partnerEnquiry)
        {
            ValidationResult<PartnerEnquiry> validationResult = new ValidationResult<PartnerEnquiry>();
            try
            {
                await _dataService.UpdateAsync(partnerEnquiry);
                validationResult.Entity = partnerEnquiry;
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
