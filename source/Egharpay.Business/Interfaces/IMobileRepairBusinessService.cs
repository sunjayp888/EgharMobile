using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IMobileRepairBusinessService
    {
        Task<ValidationResult> Create(MobileRepair mobileRepair);
        Task<IEnumerable<MobileRepair>> RetrieveMobileRepair(Expression<Func<MobileRepair, bool>> predicate);
        Task<PagedResult<MobileRepair>> RetrieveMobileRepairs(List<OrderBy> orderBy = null, Paging paging = null);
        Task<MobileRepair> RetrieveMobileRepair(int mobileRepairId);

        Task<ValidationResult> UpdateMobileRepair(int mobileRepairId, int mobileRepairStateId);
        Task<ValidationResult> CancelMobileRepairRequest(int mobileRepairId);
    }
}