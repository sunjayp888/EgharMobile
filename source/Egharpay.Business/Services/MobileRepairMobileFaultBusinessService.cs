using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;

namespace Egharpay.Business.Services
{
    public partial class MobileRepairMobileFaultBusinessService : IMobileRepairMobileFaultBusinessService
    {
        protected IMobileRepairMobileFaultDataService _dataService;
        protected IMapper _mapper;
        public MobileRepairMobileFaultBusinessService(IMobileRepairMobileFaultDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MobileRepairMobileFault>> RetrieveMobileRepairMobileFaults(int mobileRepairId)
        {
            var result = await _dataService.RetrieveAsync<MobileRepairMobileFault>(e => e.MobileRepairId == mobileRepairId);
            return _mapper.MapToList<MobileRepairMobileFault>(result);
        }

        public async Task<bool> DeleteMobileRepairMobileFault(int mobileRepairId, int mobileFaultId)
        {
            try
            {
                await _dataService.DeleteWhereAsync<MobileRepairMobileFault>(e => e.MobileRepairId == mobileRepairId && e.MobileFaultId == mobileFaultId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
