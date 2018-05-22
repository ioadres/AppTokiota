using System;
using System.Collections.Generic;
using AppTokiota.Users.Models;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services.Timeline
{
    public interface ITimeLineService
    {
        IList<TimesheetForDay> GetListTimesheetForDay(Review review);
    }
}
