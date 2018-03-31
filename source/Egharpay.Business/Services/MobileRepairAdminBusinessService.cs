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
            date = date ?? DateTime.UtcNow;
            var appointmentDateTime = date.CombineDateTime(time);
            var start = new TimeSpan(appointmentDateTime.Value.AddHours(-1).Hour, appointmentDateTime.Value.Minute, 0);
            var end = new TimeSpan(start.Hours + 3, start.Minutes, start.Seconds);

            var data =
                await _mobileRepairAdminDataService.RetrieveAsync<AvailableMobileRepairAdmin>(
                    e => !e.AppointmentDate.HasValue
                         || DbFunctions.TruncateTime(e.AppointmentDate) >= DbFunctions.TruncateTime(date));

            var result = data.Select(e => new AvailableMobileRepairAdmin()
            {
                Address = e.Address,
                AppointmentDate = e.AppointmentDate,
                PersonnelId = e.PersonnelId,
                AppointmentTime = e.AppointmentTime,
                DOB = e.DOB,
                Forenames = e.Forenames,
                MobileRepairAdminPersonnelId = e.MobileRepairAdminPersonnelId,
                Surname = e.Surname,
                Title = e.Title
            });

            var newData = result.Where(e => !e.AppointmentDate.HasValue || !(e.AppointmentDate.HasValue
                                && e.AppointmentDate.Value.IsBetween(start, end))).ToList();

            return newData;

        }

        public async Task<IEnumerable<MobileRepairAdminPersonnel>> RetrieveAvailableMobileRepairAdmins()
        {
            return await _mobileRepairAdminDataService.RetrieveAsync<MobileRepairAdminPersonnel>(e => true);
        }


    }
}
