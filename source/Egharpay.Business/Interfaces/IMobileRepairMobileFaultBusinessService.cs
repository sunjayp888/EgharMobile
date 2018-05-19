using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Mobile = Egharpay.Business.Models.Mobile;

namespace Egharpay.Business.Interfaces
{
    public interface IMobileRepairMobileFaultBusinessService
    {
        //Retrieve
        Task<IEnumerable<MobileRepairMobileFault>> RetrieveMobileRepairMobileFaults(int mobileRepairId);

        //Delete
        Task<bool> DeleteMobileRepairMobileFault(int mobileRepairId, int mobileFaultId);
    }
}
