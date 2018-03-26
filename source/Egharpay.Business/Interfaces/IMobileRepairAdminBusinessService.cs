using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IMobileRepairAdminBusinessService
    {
        //Retrieve
        Task<IEnumerable<AvailableMobileRepairAdmin>> RetrieveAvailableMobileRepairAdmin(Expression<Func<AvailableMobileRepairAdmin, bool>> predicate);
    }
}
