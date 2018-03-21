using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Users.Controls;
using AppTokiota.Users.Models;
using AppTokiota.Users.Helpers;
using AppTokiota.Users.Models.Calendar;

namespace AppTokiota.Users.Services
{
    public class TimesheetService : ITimesheetService
    {
        private IRequestService _requestService;
        public TimesheetService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<Timesheet> GetTimesheetBeetweenDates(DateTime from, DateTime to)
        {
            var url = $"{AppSettings.TimesheetUrlEndPoint}from={from.ToString("yyyy-MM-dd")}&to={to.ToString("yyyy-MM-dd")}";
            var timesheet = await _requestService.GetAsync<Timesheet>(url, AppSettings.AuthenticatedUserResponse.AccessToken);
            return timesheet;
        }
    }
}
