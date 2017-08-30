using System.Collections.Generic;
using Egharpay.Entity.Dto;

namespace Egharpay.Interfaces
{
    public interface ITenantsService
    {        
        IEnumerable<TenantOrganisation> TenantOrganisations();
        TenantOrganisation CurrentTenantOrganisation(string hostname);
    }
}
