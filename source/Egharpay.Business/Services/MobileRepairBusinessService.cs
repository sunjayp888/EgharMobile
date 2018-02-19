using System;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;

namespace Egharpay.Business.Services
{
    public class MobileRepairBusinessService : IMobileRepairBusinessService
    {
        private readonly IMobileDataService _mobileDataService;

        public MobileRepairBusinessService(IMobileDataService mobileDataService)
        {
            _mobileDataService = mobileDataService;
        }

        public async Task<ValidationResult> Create(MobileRepair mobileRepair)
        {
            var validationResult = new ValidationResult();
            try
            {
                await _mobileDataService.CreateAsync(mobileRepair);
                validationResult.Message = "Request for mobile repair created successfully.";
                validationResult.Succeeded = true;

            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Message = ex.Message;
            }
            return validationResult;
        }
    }
}
