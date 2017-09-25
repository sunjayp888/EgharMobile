using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Services
{
    public partial class NewsBusinessService : INewsBusinessService
    {
        protected INewsDataService _dataService;

        public NewsBusinessService(INewsDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<News> RetrieveNews(int newsId)
        {
            var news = await _dataService.RetrieveAsync<News>(a => a.NewsId == newsId);
            return news.FirstOrDefault();
        }

        public async Task<PagedResult<News>> RetrieveNewsList(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var newsList = await _dataService.RetrievePagedResultAsync<News>(a => true, orderBy, paging);
            return newsList;
        }
    }
}
