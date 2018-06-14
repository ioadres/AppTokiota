using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Users.Controls;
using AppTokiota.Users.Models;
using AppTokiota.Users.Helpers;
using AppTokiota.Users.Models.Calendar;
using Prism.Navigation;
using System.Diagnostics;
using Xamarin.Forms;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Polly;

namespace AppTokiota.Users.Services
{

    public class TimesheetService : TimesheetServiceBase, ITimesheetService, ISubscribeMessagingCenter
    {
        private IRequestService _requestService;
        private ICacheEntity _cacheService;

		public TimesheetService(IRequestService requestService, ICacheEntity cacheService)
          
        {
            _cacheService = cacheService;
            _requestService = requestService;
        }

        public async Task<Timesheet> GetTimesheetBeetweenDates(DateTime from, DateTime to)
        {
            try
            {
                var timesheet = await _cacheService.GetLocalObjectAsync<Timesheet>($"/{from.ToString("yyyyMMdd")}/{to.ToString("yyyyMMdd")}/timesheet");

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
            catch (Exception e)
            {
                MessagingCenter.Send<ISubscribeMessagingCenter>(this, nameof(UnauthorizedAccessException));
                Crashes.TrackError(e);
                throw e;
            }            
        }

		public async Task<TimesheetDeleteActivity> DeleteActivityTimesheet(DateTime from, int idActivity)
        {
            try 
            {
                var url = $"{AppSettings.TimesheetUrlEndPoint}/{from.ToString("yyyy")}/{from.ToString("MM")}/{idActivity}";
                var timesheet = await _requestService.DeleteAsync<TimesheetDeleteActivity>(url, AppSettings.AuthenticatedUserResponse.AccessToken);
                return timesheet;
            } 
            catch (Exception e)
            {
                MessagingCenter.Send<ISubscribeMessagingCenter>(this, nameof(UnauthorizedAccessException));
                Crashes.TrackError(e);
                throw e;
            }   
        }
        
		public async Task<Activity> PostActivity(TimesheetAddActivity timesheetAddActivity, DateTime from)
        {
            try
            {               
    			var url = $"{AppSettings.TimesheetUrlEndPoint}/{from.ToString("yyyy")}/{from.ToString("MM")}/{from.ToString("dd")}";
    			var timesheet = await _requestService.PostAsync<TimesheetAddActivity,Activity>(url,timesheetAddActivity, AppSettings.AuthenticatedUserResponse.AccessToken);
    			timesheet.ProjectId = timesheetAddActivity.ProjectId;
    			timesheet.TaskId = timesheetAddActivity.TaskId;
    			return timesheet;				
            } 
            catch (Exception e)
            {
                MessagingCenter.Send<ISubscribeMessagingCenter>(this, nameof(UnauthorizedAccessException));
                Crashes.TrackError(e);
                throw e;
            }  
        }

		public async Task<List<Activity>> BatchActivity(List<TimesheetAddActivityBatch> timesheetAddActivityBatch)
		{
            try
            {
                var url = $"{AppSettings.TimesheetUrlEndPoint}/batch";
    			var timesheet = await _requestService.PostAsync<List < TimesheetAddActivityBatch >, List<Activity>>(url, timesheetAddActivityBatch, AppSettings.AuthenticatedUserResponse.AccessToken);
                return timesheet;    		
            }
            catch (Exception e)
            {
                MessagingCenter.Send<ISubscribeMessagingCenter>(this, nameof(UnauthorizedAccessException));
                Crashes.TrackError(e);
                throw e;
            }
        }
    }
}
