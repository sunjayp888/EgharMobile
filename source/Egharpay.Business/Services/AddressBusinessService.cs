using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;

namespace Egharpay.Business.Services
{
    public class AddressBusinessService : IAddressBusinessService
    {
        private readonly IAddressDataService _addressDataService;

        public AddressBusinessService(IAddressDataService addressDataService)
        {
            _addressDataService = addressDataService;
        }

        public async Task<ValidationResult> CreateAddress(int personnelId, Address address)
        {
            try
            {
                address.PersonnelId = personnelId;
                address.CreatedDateTime = DateTime.Now;
                await _addressDataService.CreateAsync(address);
                return new ValidationResult { Succeeded = true };
            }
            catch (Exception e)
            {
                return new ValidationResult { Succeeded = false, Message = e.Message };
            }

        }

        public async Task<IEnumerable<Address>> RetrieveAddresses(int personnelId)
        {
            return await _addressDataService.RetrieveAsync<Address>(e => e.PersonnelId == personnelId);
        }

        public async Task<ValidationResult> RemovePersonnelAddress(int personnelId, int addressId)
        {
            var addressResult = await _addressDataService.RetrieveAsync<Address>(e => e.PersonnelId == personnelId && e.AddressId == addressId);
            try
            {
                var address = addressResult.FirstOrDefault();
                if (address != null)
                    await _addressDataService.DeleteAsync(address);
                return new ValidationResult() { Succeeded = true };
            }
            catch (Exception e)
            {
                return new ValidationResult() { Succeeded = false, Message = e.Message };
            }
        }
    }
}
