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
    public interface ITrendCommentBusinessService
    {
        //Create
        Task<ValidationResult<TrendComment>> CreateTrendComment(TrendComment trendComment);

        //Retrieve
        Task<TrendComment> RetrieveTrendComment(int trendCommentId);
        Task<PagedResult<TrendComment>> RetrieveTrendComments(List<OrderBy> orderBy = null, Paging paging = null);
        //Task<PagedResult<TrendComment>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);

        //Update
        Task<ValidationResult<TrendComment>> UpdateTrendComment(TrendComment trendComment);
    }
}
