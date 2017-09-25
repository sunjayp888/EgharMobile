using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Data.Interfaces;

namespace Egharpay.Data.Services
{
    public class AdvertiseDataService:EgharpayDataService,IAdvertiseDataService
    {
        public AdvertiseDataService(IDatabaseFactory<EgharpayDatabase> databaseFactory, IGenericDataService<DbContext> genericDataService) : base(databaseFactory, genericDataService)
        {
        }
    }
}
