using AppTokiota.Users.Controls;
using AppTokiota.Users.Helpers;
using AppTokiota.Users.Models;
using AppTokiota.Users.Models.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services
{
    public class CalendarService : ICalendarService
    {
        public async Task<IList<SpecialDate>> GetSpecialDatesBeetweenDatesAsync(Timesheet timesheet)
        {
            var specialDates = new List<SpecialDate>();
            var modelDay = GetSpecialDaysByModelDay(timesheet.Days).ToList();
            var modelImputed = GetSpecialDaysByModelImputed(timesheet.Activities).ToList();
            var modelDayClose = GetSpecialDaysByCloseDay(timesheet.Days);          

            specialDates.AddRange(modelDayClose);

            foreach(var item in modelImputed) {
                if (specialDates.Any(x => x.Date.Equals(item.Date))) continue;
                specialDates.Add(item);
            }

            foreach (var item in modelDay)
            {
                if (specialDates.Any(x => x.Date.Equals(item.Date))) continue;
                specialDates.Add(item);
            }


            return await Task.FromResult(specialDates);
        }

        private List<SpecialDate> GetSpecialDaysByModelImputed(Dictionary<int, Activity> activities)
        {
            var listSpecialDate = new List<SpecialDate>();
            try
            {
                var dayStyle = new List<DayStyle>() { CalendarColors.GetImputed() };
                foreach (var item in activities)
                {
                    var specialDay = new SpecialDate(item.Value.Date, true);
                    listSpecialDate.Add(CalendarColors.GetSpecialDay(specialDay, item.Value.Date, dayStyle));                   
                }
            }
            catch (Exception e)
            {
                var t = e;
            }

            return listSpecialDate;
        }

        private List<SpecialDate> GetSpecialDaysByModelDay(List<Day> days)
        {
            var listSpecialDate = new List<SpecialDate>();
            foreach (var item in days)
            {
                var specialDay = new SpecialDate(item.Date, true);
                var dayStyle = new List<DayStyle>();
                
                if (item.Holiday != null)
                {
                    dayStyle.Add(CalendarColors.GetHoliday());
                }
				if (item.IsWeekend && item.Holiday == null)
                {
                    dayStyle.Add(CalendarColors.GetWeekend());
                }

                listSpecialDate.Add(CalendarColors.GetSpecialDay(specialDay, item.Date, dayStyle));
            }
            return listSpecialDate;
        }

        private List<SpecialDate> GetSpecialDaysByCloseDay(List<Day> days)
        {
            var listSpecialDate = new List<SpecialDate>();
            foreach (var item in days.Where(x=>x.IsClosed))
            {
                var specialDay = new SpecialDate(item.Date, true);
                var dayStyle = new List<DayStyle>();
                dayStyle.Add(CalendarColors.SetCloseDay());
                listSpecialDate.Add(CalendarColors.GetSpecialDay(specialDay, item.Date, dayStyle));
            }
            return listSpecialDate;
        }
    }
}
