using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Controls;
using AppTokiota.Models;
using AppTokiota.Helpers;
using AppTokiota.Models.Calendar;

namespace AppTokiota.Services
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
