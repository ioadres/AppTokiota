using AppTokiota.Controls;
using AppTokiota.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Services
{
    public interface ITimesheetService
    {
        Task<Timesheet> GetTimesheetBeetweenDates(DateTime from, DateTime To);
        Task<IList<SpecialDate>> GetSpecialDatesBeetweenDates(DateTime from, DateTime To);
    }
}
