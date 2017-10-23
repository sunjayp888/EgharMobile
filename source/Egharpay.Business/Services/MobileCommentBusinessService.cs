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
    public partial class MobileCommentBusinessService:IMobileCommentBusinessService
    {
        protected IMobileCommentDataService _dataService;

        public MobileCommentBusinessService(IMobileCommentDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<ValidationResult<MobileComment>> CreateMobileComment(MobileComment mobileComment)
        {
            ValidationResult<MobileComment> validationResult = new ValidationResult<MobileComment>();
            try
            {
                await _dataService.CreateAsync(mobileComment);
                validationResult.Entity = mobileComment;
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

        public async Task<MobileComment> RetrieveMobileComment(int mobileCommentId)
        {
            var mobileComment = await _dataService.RetrieveAsync<MobileComment>(a => a.MobileCommentId == mobileCommentId);
            return mobileComment.FirstOrDefault();
        }

        public async Task<PagedResult<MobileCommentGrid>> RetrieveMobileComments(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var mobileComments = await _dataService.RetrievePagedResultAsync<MobileCommentGrid>(a => true, orderBy, paging);
            return mobileComments;
        }

        public async Task<ValidationResult<MobileComment>> UpdateMobileComment(MobileComment mobileComment)
        {
            ValidationResult<MobileComment> validationResult = new ValidationResult<MobileComment>();
            try
            {
                await _dataService.UpdateAsync(mobileComment);
                validationResult.Entity = mobileComment;
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
