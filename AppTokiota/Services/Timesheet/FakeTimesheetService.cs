using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Models;
using Xamarin.Forms;
using AppTokiota.Controls;

namespace AppTokiota.Services
{
    public class FakeTimesheetService : ITimesheetService
    {
        public async Task<Timesheet> GetTimesheetBeetweenDates(DateTime from, DateTime To)
        {
            await Task.Delay(2000);
            var dateNow = DateTime.Now;
            Random rnd = new Random();

            var projects = new List<Project>();
            var task = new List<TaskT>();
            var days = new List<Day>();
            var activities = new List<Activity>();


            var dateN = new DateTime(dateNow.Year, dateNow.Month, 1);
            for (var i = 0; i < 30; i++)
            {
                days.Add(new Day()
                {
                    Date = dateN.AddDays(i),
                    Holiday = rnd.Next() % 6 == 0 ? true : false,
                    IsClosed = false,
                    IsWeekend = false,
                    name = "nameDay"
                });
            }

            dateN = new DateTime(dateNow.Year, dateNow.Month, 1);
            for (var i = 0; i < 15; i++)
            {
                activities.Add(new Activity()
                {
                    Date = dateN.AddDays(i).ToString()  
                });
            }           

            var timesheet = new Timesheet();
            timesheet.Activities = activities;
            timesheet.Days = days;
            timesheet.Projects = projects;

            return timesheet;
        }

        public async Task<IList<SpecialDate>> GetSpecialDatesBeetweenDates(DateTime from, DateTime To)
        {
            var timesheet = await GetTimesheetBeetweenDates(from, To);
            var holidays = timesheet.Days.Where(x => x.Holiday == true);
            var close = timesheet.Days.Where(x => x.IsClosed);

            var specialDates = new List<SpecialDate>();

            var white_row = new Pattern { WidthPercent = 1f, HightPercent = 0.04f, Color = Color.Transparent };
            var white_col = new Pattern { WidthPercent = 0.04f, HightPercent = 1f, Color = Color.Transparent };
            holidays.ForEach(x => specialDates.Add(new SpecialDate(x.Date)
            {
                BackgroundColor = Color.Black,
                TextColor = Color.Red,
                Selectable = true,
                FontSize= 15,
                BackgroundPattern = new BackgroundPattern(1)
                {
                    Pattern = new List<Pattern>
                            {
                                new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Red},
                                new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Purple},
                                new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Green},
                                new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Yellow}
                            }
                }

            }));

            return specialDates;            
        }
    }
}
