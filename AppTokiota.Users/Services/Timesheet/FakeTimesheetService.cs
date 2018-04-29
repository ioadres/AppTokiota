using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Users.Models;
using Xamarin.Forms;
using AppTokiota.Users.Controls;
using AppTokiota.Users.Helpers;
using AppTokiota.Users.Models.Calendar;
using Newtonsoft.Json;

namespace AppTokiota.Users.Services
{
    public class FakeTimesheetService : TimesheetServiceBase, ITimesheetService
    {
        public async Task<Timesheet> GetTimesheetBeetweenDates(DateTime from, DateTime To)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<Timesheet>(FakeTimesheetData.Timesheet));
        }
    }
}
