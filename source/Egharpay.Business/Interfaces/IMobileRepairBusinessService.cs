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
        #region Create
        Task<ValidationResult> Create(MobileRepair mobileRepair);
        Task<ValidationResult> CreateMobileRepairPayment(MobileRepairPayment mobileRepairPayment);
        #endregion

        #region Retrieve
        Task<IEnumerable<MobileRepair>> RetrieveMobileRepair(Expression<Func<MobileRepair, bool>> predicate);
        Task<PagedResult<MobileRepair>> RetrieveMobileRepairs(List<OrderBy> orderBy = null, Paging paging = null);
        Task<MobileRepair> RetrieveMobileRepair(int mobileRepairId);
        Task<PagedResult<MobileRepairGrid>> RetrieveMobileRepairGrids(Expression<Func<MobileRepairGrid, bool>> predicate,List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<MobileFault>> RetrieveMobileFaults(List<OrderBy> orderBy = null, Paging paging = null);
        #endregion

        #region Update
        Task<ValidationResult> UpdateMobileRepairState(int mobileRepairId, int mobileRepairStateId);
        Task<ValidationResult> CancelMobileRepairRequest(int mobileRepairId);
        Task<ValidationResult<MobileRepair>> UpdateMobileRepair(MobileRepair mobileRepair, List<int> mobileFaultIds);
        #endregion
    }
}