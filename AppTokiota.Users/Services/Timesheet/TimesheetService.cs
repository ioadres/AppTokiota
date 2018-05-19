using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Users.Controls;
using AppTokiota.Users.Models;
using AppTokiota.Users.Helpers;
using AppTokiota.Users.Models.Calendar;
using AppTokiota.Users.Services.Cache;
using Prism.Navigation;
using System.Diagnostics;

namespace AppTokiota.Users.Services
{

    public class TimesheetService : TimesheetServiceBase, ITimesheetService
    {
        private IRequestService _requestService;
        private ICacheEntity _cacheService;
		private IAuthenticationService _authenticationService;

		public TimesheetService(IRequestService requestService, ICacheEntity cacheService, IAuthenticationService authentication)
          
        {
            _cacheService = cacheService;
            _requestService = requestService;
			_authenticationService = authentication;
        }

        public async Task<Timesheet> GetTimesheetBeetweenDates(DateTime from, DateTime to)
        {
			for (var i = 0; i < 2; i++)
			{
				try
				{
					if (await _authenticationService.UserIsAuthenticatedAndValidAsync())
					{
						var url = $"{AppSettings.TimesheetUrlEndPoint}?from={from.ToString("yyyy-MM-dd")}&to={to.ToString("yyyy-MM-dd")}";
						var timesheet = await _requestService.GetAsync<Timesheet>(url, AppSettings.AuthenticatedUserResponse.AccessToken);
						return timesheet;
					}
					else {
						throw new UnauthorizedAccessException();
					}
				}
				catch (Exception ex)
				{
					i++;
					AppSettings.AuthenticatedUserResponse = null;
					Debug.WriteLine(ex);
				}               
			}
			throw new UnauthorizedAccessException();
               
        }

		public async Task<TimesheetDeleteActivity> DeleteActivityTimesheet(DateTime from, int idActivity)
        {
			var url = $"{AppSettings.TimesheetUrlEndPoint}/{from.ToString("yyyy")}/{from.ToString("MM")}/{idActivity}";
			var timesheet = await _requestService.DeleteAsync<TimesheetDeleteActivity>(url, AppSettings.AuthenticatedUserResponse.AccessToken);
            return timesheet;         
        }
        
		public async Task<Activity> PostActivity(TimesheetAddActivity timesheetAddActivity, DateTime from)
        {
			var url = $"{AppSettings.TimesheetUrlEndPoint}/{from.ToString("yyyy")}/{from.ToString("MM")}/{from.ToString("dd")}";
			var timesheet = await _requestService.PostAsync<TimesheetAddActivity,Activity>(url,timesheetAddActivity, AppSettings.AuthenticatedUserResponse.AccessToken);
			timesheet.ProjectId = timesheetAddActivity.ProjectId;
			timesheet.TaskId = timesheetAddActivity.TaskId;
            return timesheet;
        }

		public async Task<List<Activity>> BatchActivity(List<TimesheetAddActivityBatch> timesheetAddActivityBatch)
        {
            var url = $"{AppSettings.TimesheetUrlEndPoint}/batch";
			var timesheet = await _requestService.PostAsync<List < TimesheetAddActivityBatch >, List<Activity>>(url, timesheetAddActivityBatch, AppSettings.AuthenticatedUserResponse.AccessToken);
            return timesheet;
        }
    }
}
