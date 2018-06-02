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
            var timesheet = await _cacheService.GetLocalObjectAsync<Timesheet>($"/{from.ToString("yyyyMMdd")}/{to.ToString("yyyyMMdd")}/timesheet");

			for (var i = 0; i < 2; i++)
			{
				try
				{
					if (await _authenticationService.UserIsAuthenticatedAndValidAsync())
					{
                        if (timesheet == null || AppSettings.IsEnableCache == false)
                        {
                            var nowLess3Month = DateTime.Now.AddMonths(-3);

                            var url = $"{AppSettings.TimesheetUrlEndPoint}?from={from.ToString("yyyy-MM-dd")}&to={to.ToString("yyyy-MM-dd")}";
                            timesheet = await _requestService.GetAsync<Timesheet>(url, AppSettings.AuthenticatedUserResponse.AccessToken);
                            if (to < nowLess3Month)
                            {
                                await _cacheService.InsertLocalObjectAsync($"/{from.ToString("yyyyMMdd")}/{to.ToString("yyyyMMdd")}/timesheet",timesheet);
                            }

                        }

                        return timesheet;
					}
					else {
						throw new UnauthorizedAccessException();
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex);
				}               
			}
			throw new UnauthorizedAccessException();
               
        }

		public async Task<TimesheetDeleteActivity> DeleteActivityTimesheet(DateTime from, int idActivity)
        {
			for (var i = 0; i < 2; i++)
            {
                try
                {
                    if (await _authenticationService.UserIsAuthenticatedAndValidAsync())
                    {
            			var url = $"{AppSettings.TimesheetUrlEndPoint}/{from.ToString("yyyy")}/{from.ToString("MM")}/{idActivity}";
            			var timesheet = await _requestService.DeleteAsync<TimesheetDeleteActivity>(url, AppSettings.AuthenticatedUserResponse.AccessToken);
						return timesheet;   
					}
                    else
                    {
                        throw new UnauthorizedAccessException();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            throw new UnauthorizedAccessException();      
        }
        
		public async Task<Activity> PostActivity(TimesheetAddActivity timesheetAddActivity, DateTime from)
        {
			for (var i = 0; i < 2; i++)
            {
                try
                {
                    if (await _authenticationService.UserIsAuthenticatedAndValidAsync())
                    {
            			var url = $"{AppSettings.TimesheetUrlEndPoint}/{from.ToString("yyyy")}/{from.ToString("MM")}/{from.ToString("dd")}";
            			var timesheet = await _requestService.PostAsync<TimesheetAddActivity,Activity>(url,timesheetAddActivity, AppSettings.AuthenticatedUserResponse.AccessToken);
            			timesheet.ProjectId = timesheetAddActivity.ProjectId;
            			timesheet.TaskId = timesheetAddActivity.TaskId;
        				return timesheet;
					}
                    else
                    {
                        throw new UnauthorizedAccessException();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            throw new UnauthorizedAccessException();
        }

		public async Task<List<Activity>> BatchActivity(List<TimesheetAddActivityBatch> timesheetAddActivityBatch)
		{
			for (var i = 0; i < 2; i++)
            {
                try
                {
                    if (await _authenticationService.UserIsAuthenticatedAndValidAsync())
                    {
                        var url = $"{AppSettings.TimesheetUrlEndPoint}/batch";
            			var timesheet = await _requestService.PostAsync<List < TimesheetAddActivityBatch >, List<Activity>>(url, timesheetAddActivityBatch, AppSettings.AuthenticatedUserResponse.AccessToken);
                        return timesheet;
            		}
                    else
                    {
                        throw new UnauthorizedAccessException();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            throw new UnauthorizedAccessException();
        }
    }
}
