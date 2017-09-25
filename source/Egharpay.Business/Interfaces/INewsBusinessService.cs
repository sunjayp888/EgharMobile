using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface INewsBusinessService
    {
        //Retrieve
        Task<News> RetrieveNews(int newsId);
        Task<PagedResult<News>> RetrieveNewsList(List<OrderBy> orderBy = null, Paging paging = null);
    }
}
