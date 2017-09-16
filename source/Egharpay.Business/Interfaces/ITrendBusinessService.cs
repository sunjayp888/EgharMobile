using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface ITrendBusinessService
    {
        //Create
        Task<ValidationResult<Trend>> CreateTrend(Trend trend);

        //Retrieve
        Task<Trend> RetrieveTrend(int trendId);
        Task<PagedResult<Trend>> RetrieveTrends(List<OrderBy> orderBy = null, Paging paging = null);
        //Task<PagedResult<Trend>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);
    }
}
