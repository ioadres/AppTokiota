using System;
using System.Collections.ObjectModel;
using System.Linq;
using AppTokiota.Components.Core;
using AppTokiota.Components.Core.Module;
using Prism.Commands;
using Prism.Navigation;
using AppTokiota.Controls;
using System.Threading.Tasks;

namespace AppTokiota.Components.Timesheet
{
    public class TimesheetPageViewModel : ViewModelBase
    {
        private readonly ITimesheetModule _timesheetModule;

        private ObservableCollection<DateTime> _dates;

        public ObservableCollection<DateTime> Dates
        {
            get { return _dates; }
            set { SetProperty(ref _dates, value); }
        }

        private ObservableCollection<SpecialDate> _specialDates;
        public ObservableCollection<SpecialDate> SpecialDates
        {
            get { return _specialDates; }
            set { SetProperty(ref _specialDates, value); }
        }
        

        public DelegateCommand<object> SignOutCommand => new DelegateCommand<object>(SelectedDate);

        public TimesheetPageViewModel(INavigationService navigationService, ITimesheetModule timesheetModule) : base(navigationService)
        {
            _timesheetModule = timesheetModule;
            Title = "Timesheet";

            var today = DateTime.Today;

            _dates = new ObservableCollection<DateTime>
            {
                today
            };

            LoadSpecialDatesAsync();

            SelectedDate(today);

        }

        private async void LoadSpecialDatesAsync()
        {
            SpecialDates = new ObservableCollection<SpecialDate>();
            DateTime date = DateTime.Now;
            var from = new DateTime(date.Year, date.Month, 1);
            var to = from.AddMonths(1).AddDays(-1);
            var result = await _timesheetModule.TimesheetService.GetSpecialDatesBeetweenDates(from, to);
            result.ForEach(x => SpecialDates.Add(x));
        }
            
        private void SelectedDate(object date)
        {
            if (date == null)
                return;

            if (Dates.Any())
            {

            }
        }
    }
}