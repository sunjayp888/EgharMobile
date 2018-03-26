using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Services
{
    public partial class MobileRepairAdminBusinessService : IMobileRepairAdminBusinessService
    {
        private readonly IMobileRepairAdminDataService _mobileRepairAdminDataService;

        public MobileRepairAdminBusinessService(IMobileRepairAdminDataService mobileRepairAdminDataService)
        {
            _mobileRepairAdminDataService = mobileRepairAdminDataService;
        }


        public async Task<IEnumerable<AvailableMobileRepairAdmin>> RetrieveAvailableMobileRepairAdmin(Expression<Func<AvailableMobileRepairAdmin, bool>> predicate)
        {
            return await _mobileRepairAdminDataService.RetrieveAsync<AvailableMobileRepairAdmin>(predicate);
        }


    }
}
