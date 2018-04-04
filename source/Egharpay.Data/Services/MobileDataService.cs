using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Egharpay.Data.Extensions;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Data.Services
{
    public class MobileDataService : EgharpayDataService, IMobileDataService
    {
        public MobileDataService(IDatabaseFactory<EgharpayDatabase> databaseFactory, IGenericDataService<DbContext> genericDataService) : base(databaseFactory, genericDataService)
        {
        }

        public IQueryable<Search> Search(string term = null, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScopeAsync)
            using (var context = _databaseFactory.CreateContext())
            {
                var search = context.Searches.
                 SqlQuery("[dbo].[Search] @SearchKeyword", new SqlParameter("SearchKeyword", term))
                  .AsQueryable();
                return search;
            }
        }

    }
}
