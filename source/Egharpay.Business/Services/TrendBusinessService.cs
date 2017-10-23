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
    public partial class TrendBusinessService : ITrendBusinessService
    {
        protected ITrendDataService _dataService;

        public TrendBusinessService(ITrendDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<ValidationResult<Trend>> CreateTrend(Trend trend)
        {
            ValidationResult<Trend> validationResult = new ValidationResult<Trend>();
            try
            {
                await _dataService.CreateAsync(trend);
                validationResult.Entity = trend;
                validationResult.Succeeded = true;
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

        public async Task<Trend> RetrieveTrend(int trendId)
        {
            var trend = await _dataService.RetrieveAsync<Trend>(a => a.TrendId == trendId);
            return trend.FirstOrDefault();
        }

        public async Task<PagedResult<Trend>> RetrieveTrends(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var trends = await _dataService.RetrievePagedResultAsync<Trend>(a => true, orderBy, paging);
            return trends;
        }

        //public async Task<PagedResult<Trend>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null)
        //{
        //    return await _dataService.RetrievePagedResultAsync<Trend>(a => a.SearchField.ToLower().Contains(term.ToLower()), orderBy, paging);
        //}
    }
}
