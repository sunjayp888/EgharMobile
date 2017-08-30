using System;
using System.Collections.Generic;
using System.Linq;
using Egharpay.Business.Interfaces;
using Egharpay.Entity.Dto;
using Egharpay.Interfaces;

namespace Egharpay
{
    public class TenantsService : ITenantsService
    {
        private ICacheProvider _cacheProvider;
        private ITenantOrganisationService _tenantOrganisationService;
        private const string TenantOrganisationsCacheKey = "TenantOrganisations";

        public TenantsService(ICacheProvider cacheProvider, ITenantOrganisationService tenantOrganisationService)
        {
            _cacheProvider = cacheProvider;
            _tenantOrganisationService = tenantOrganisationService;
        }

        private void LoadTenantsCache()
        {
            var tenants = _tenantOrganisationService.RetrieveTenantOrganisations();
            _cacheProvider.Set(TenantOrganisationsCacheKey, tenants, 1440);
        }

        public IEnumerable<TenantOrganisation> TenantOrganisations()
        {
            if (!_cacheProvider.IsSet(TenantOrganisationsCacheKey))
                LoadTenantsCache();

            return _cacheProvider.Get<IEnumerable<TenantOrganisation>>(TenantOrganisationsCacheKey);
        }

        public TenantOrganisation CurrentTenantOrganisation(string hostname)
        {
            hostname = "nidanserver";
            return TenantOrganisations().SingleOrDefault(t => t.HostName.Equals(hostname, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}