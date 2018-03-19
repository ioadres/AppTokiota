using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Controls;
using AppTokiota.Models;

namespace AppTokiota.Services
{
    public class TimesheetService : ITimesheetService
    {
        public Task<IList<SpecialDate>> GetSpecialDatesBeetweenDates(DateTime from, DateTime To)
        {
            throw new NotImplementedException();
        }

        public Timesheet GetTimesheetBeetweenDates(DateTime from, DateTime To)
        {
            throw new NotImplementedException();
        }

        Task<Timesheet> ITimesheetService.GetTimesheetBeetweenDates(DateTime from, DateTime To)
        {
            throw new NotImplementedException();
        }
    }
}
