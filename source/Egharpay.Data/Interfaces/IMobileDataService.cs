using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Data.Interfaces
{
    public interface IMobileDataService : IEgharpayDataService
    {
        IQueryable<Search> Search(string term = null, List<OrderBy> orderBy = null, Paging paging = null);
    }
}
