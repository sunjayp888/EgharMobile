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
    public partial class TrendCommentBusinessService :ITrendCommentBusinessService
    {
        protected ITrendCommentDataService _dataService;

        public TrendCommentBusinessService(ITrendCommentDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<ValidationResult<TrendComment>> CreateTrendComment(TrendComment trendComment)
        {
            ValidationResult<TrendComment> validationResult = new ValidationResult<TrendComment>();
            try
            {
                await _dataService.CreateAsync(trendComment);
                validationResult.Entity = trendComment;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            validationResult.Succeeded = true;
            return validationResult;
        }

        public async Task<TrendComment> RetrieveTrendComment(int trendCommentId)
        {
            var trendComment = await _dataService.RetrieveAsync<TrendComment>(a => a.TrendCommentId == trendCommentId);
            return trendComment.FirstOrDefault();
        }

        public async Task<PagedResult<TrendCommentGrid>> RetrieveTrendComments(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var trendComments = await _dataService.RetrievePagedResultAsync<TrendCommentGrid>(a => true, orderBy, paging);
            return trendComments;
        }

        public async Task<ValidationResult<TrendComment>> UpdateTrendComment(TrendComment trendComment)
        {
            ValidationResult<TrendComment> validationResult = new ValidationResult<TrendComment>();
            try
            {
                await _dataService.UpdateAsync(trendComment);
                validationResult.Entity = trendComment;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        //public async Task<PagedResult<TrendComment>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
