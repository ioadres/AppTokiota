using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Models;
using Xamarin.Forms;
using AppTokiota.Controls;
using AppTokiota.Helpers;
using AppTokiota.Models.Calendar;
using Newtonsoft.Json;

namespace AppTokiota.Services
{
    public class FakeTimesheetService : ITimesheetService
    {
        public async Task<Timesheet> GetTimesheetBeetweenDates(DateTime from, DateTime To)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<Timesheet>(FakeTimesheetData.Timesheet));
        }        

    }
}
