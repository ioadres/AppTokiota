using System;
using System.Collections.Generic;
using AppTokiota.Users.Models;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services
{
    public interface ITimeLineService
    {
        Task<IList<ItemTimeLine>> GetListTimesheetForDay(Review review);
    }
}
