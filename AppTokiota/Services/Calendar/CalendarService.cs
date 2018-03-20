using AppTokiota.Controls;
using AppTokiota.Helpers;
using AppTokiota.Models;
using AppTokiota.Models.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Services
{
    public class CalendarService : ICalendarService
    {
        public async Task<IList<SpecialDate>> GetSpecialDatesBeetweenDatesAsync(Timesheet timesheet)
        {
            var specialDates = new List<SpecialDate>();
            var modelDay = GetSpecialDaysByModelDay(timesheet.Days).ToList();
            var modelImputed = GetSpecialDaysByModelInputed(timesheet.Activities).ToList();
            var modelDayClose = GetSpecialDaysByCloseDay(timesheet.Days);
           
            

            specialDates.AddRange(modelDay);
            specialDates.AddRange(modelImputed);
            specialDates.AddRange(modelDayClose);

            return await Task.FromResult(specialDates);
        }

        private List<SpecialDate> GetSpecialDaysByModelInputed(Dictionary<int, Activity> activities)
        {
            var listSpecialDate = new List<SpecialDate>();
            try
            {
                var dayStyle = CalendarColors.GetInputed();
                foreach (var item in activities)
                {
                    listSpecialDate.Add(new SpecialDate(item.Value.Date)
                    {
                        BackgroundColor = dayStyle.BackgroundColor,
                        TextColor = dayStyle.TextColor,
                        FontFamily = dayStyle.FontFamily,
                        FontAttributes = dayStyle.FontAttributes,
                        BorderWidth = dayStyle.BorderWidth,
                        BackgroundImage = dayStyle.BackgroundImage,
                        FontSize = dayStyle.FontSize
                    });
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
                var specialDay = new SpecialDate(item.Date, !item.IsClosed);
                var dayStyle = new List<DayStyle>();
                if (item.IsWeekend)
                {
                    dayStyle.Add(CalendarColors.GetWeekend());
                }
                if (item.Holiday != null)
                {
                    dayStyle.Add(CalendarColors.GetHoliday());
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
                var specialDay = new SpecialDate(item.Date, !item.IsClosed);
                var dayStyle = new List<DayStyle>();
                dayStyle.Add(CalendarColors.SetCloseDay());
                listSpecialDate.Add(CalendarColors.GetSpecialDay(specialDay, item.Date, dayStyle));
            }
            return listSpecialDate;
        }
    }
}
