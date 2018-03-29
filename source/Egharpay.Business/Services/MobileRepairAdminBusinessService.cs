using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Business.Extensions;

namespace Egharpay.Business.Services
{
    public partial class MobileRepairAdminBusinessService : IMobileRepairAdminBusinessService
    {
        private readonly IMobileRepairAdminDataService _mobileRepairAdminDataService;

        public MobileRepairAdminBusinessService(IMobileRepairAdminDataService mobileRepairAdminDataService)
        {
            _mobileRepairAdminDataService = mobileRepairAdminDataService;
        }


        public async Task<IEnumerable<AvailableMobileRepairAdmin>> RetrieveAvailableMobileRepairAdmin(DateTime? date, string time)
        {
            var appointmentDateTime = date.CombineDateTime(time);
            var startTime = appointmentDateTime.Value.AddHours(-1);
            var endTime = appointmentDateTime.Value.AddHours(3);
            TimeSpan start = new TimeSpan(appointmentDateTime.Value.AddHours(-1).Hour, appointmentDateTime.Value.Minute, 0);
            TimeSpan end = new TimeSpan(start.Hours + 3, start.Minutes, start.Seconds);
            return await _mobileRepairAdminDataService.RetrieveAsync<AvailableMobileRepairAdmin>
                (e => !e.AppointmentDate.HasValue || (e.AppointmentDate.HasValue && e.AppointmentDate.Value.TimeOfDay >= start && e.AppointmentDate.Value.TimeOfDay <= end));

        }




    }
}
