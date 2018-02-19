using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Data.Interfaces;

namespace Egharpay.Data.Services
{
    public class CouponDataService : EgharpayDataService,ICouponDataService
    {
        public CouponDataService(IDatabaseFactory<EgharpayDatabase> databaseFactory, IGenericDataService<DbContext> genericDataService) : base(databaseFactory, genericDataService)
        {
        }
    }
}
