using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Services
{
    public partial class MaintenanceBusinessService : IMaintenanceBusinessService
    {
        protected IMaintenanceDataService _dataService;

        public MaintenanceBusinessService(IMaintenanceDataService dataService)
        {
            _dataService = dataService;
        }

        #region Create

        public Task<ValidationResult<Maintenance>> CreateMaintenance(Maintenance maintenance)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Retrieve

        public async Task<Maintenance> RetrieveMaintenance(int centreId, int maintenanceId)
        {
            var maintenances = await _dataService.RetrieveAsync<Maintenance>(a => a.MaintenanceId == maintenanceId && (a.CentreId == centreId));
            return maintenances.FirstOrDefault();
        }

        public async Task<PagedResult<MaintenanceGrid>> RetrieveMaintenances(int centreId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var maintenance = await _dataService.RetrievePagedResultAsync<MaintenanceGrid>(a => a.CentreId == centreId, orderBy, paging);
            return maintenance;
        }

        public async Task<PagedResult<MaintenanceGrid>> Search(int centreId, string term, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return await _dataService.RetrievePagedResultAsync<MaintenanceGrid>(a => a.CentreId == centreId && a.SearchField.ToLower().Contains(term.ToLower()), orderBy, paging);
        }

        #endregion

        #region Update

        public Task<ValidationResult<Maintenance>> UpdateMaintenance(Maintenance maintenance)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Delete

        public async Task<bool> CanDeleteMaintenance(int maintenanceId)
        {
            return true;
        }

        public async Task<bool> DeleteMaintenance(int maintenanceId)
        {
            try
            {
                await _dataService.DeleteByIdAsync<Maintenance>(maintenanceId);
                return true;
            }
            catch
            {
                return false;
            }
        }
     
        #endregion
    }
}
