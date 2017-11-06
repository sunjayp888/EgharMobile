using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;

namespace Egharpay.Data.Services
{
    public class SellerMobileDataService : EgharpayDataService, ISellerMobileDataService
    {
        public SellerMobileDataService(IDatabaseFactory<EgharpayDatabase> databaseFactory, IGenericDataService<DbContext> genericDataService) : base(databaseFactory, genericDataService)
        {
        }
    }
}
