using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IMaintenanceBusinessService
    {
        //Create
        Task<ValidationResult<Maintenance>> CreateMaintenance(Maintenance maintenance);

        //Retrieve
        Task<bool> CanDeleteMaintenance(int maintenanceId);
        Task<Maintenance> RetrieveMaintenance(int centreId, int maintenanceId);
        Task<PagedResult<MaintenanceGrid>> RetrieveMaintenances(int centreId, List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<MaintenanceGrid>> Search(int centreId, string term, List<OrderBy> orderBy = null, Paging paging = null);

        //Update
        Task<ValidationResult<Maintenance>> UpdateMaintenance(Maintenance maintenance);

        //Delete
        Task<bool> DeleteMaintenance(int maintenanceId);
    }
}
