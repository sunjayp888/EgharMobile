using System.Collections.Generic;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface ITenantOrganisationService
    {
        IEnumerable<TenantOrganisation> RetrieveTenantOrganisations();
    }
}
