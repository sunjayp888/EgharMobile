using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;

namespace Egharpay.Business.Services
{
    public class MobileRepairFeeBusinessService : IMobileRepairFeeBusinessService
    {
        private readonly IMobileBusinessService _mobileBusinessService;
        private readonly IMobileRepairFeeDataService _mobileRepairFeeDataService;

        public MobileRepairFeeBusinessService(IMobileBusinessService mobileBusinessService, IMobileRepairFeeDataService mobileRepairFeeDataService)
        {
            _mobileBusinessService = mobileBusinessService;
            _mobileRepairFeeDataService = mobileRepairFeeDataService;
        }

        public async Task Create()
        {
            var mobiles = await _mobileBusinessService.RetrieveMobiles(e => true);

            foreach (var mobile in mobiles.Items)
            {
                var mobileFault = new List<MobileRepairFee>
                {
                    new MobileRepairFee() {MobileFaultId = 1,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 2,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 3,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 4,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 5,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 6,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 7,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 8,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 9,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 10,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 11,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 12,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 13,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 14,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 15,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 16,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 17,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 18,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 19,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 20,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 21,MobileId =mobile.MobileId },
                    new MobileRepairFee() {MobileFaultId = 22,MobileId =mobile.MobileId }
                };
                try
                {
                    await _mobileRepairFeeDataService.CreateRangeAsync(mobileFault);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
        }

        public async Task<IEnumerable<MobileRepairFeeGrid>> RetrieveMobileRepairFeeGrid(int brandId, int mobileId)
        {
            return await _mobileRepairFeeDataService.RetrieveAsync<MobileRepairFeeGrid>(e => e.BrandId == brandId && e.MobileId == mobileId);
        }
    }
}
