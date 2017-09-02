using System.Data.Entity;
using Egharpay.Data.Interfaces;
using Egharpay.Data.Models;

namespace Egharpay.Data.Services
{
    public class PersonnelDataService : EgharpayDataService, IPersonnelDataService
    {
        public PersonnelDataService(IDatabaseFactory<EgharpayDatabase> databaseFactory, IGenericDataService<DbContext> genericDataService) : base(databaseFactory, genericDataService)
        {
        }
    }
}
