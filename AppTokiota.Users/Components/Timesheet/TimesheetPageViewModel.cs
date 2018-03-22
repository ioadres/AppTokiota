using System;
using System.Collections.ObjectModel;
using System.Linq;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using Prism.Commands;
using Prism.Navigation;
using AppTokiota.Users.Controls;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using System.Windows.Input;

namespace AppTokiota.Users.Components.Timesheet
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

        public Command DateChosen => new Command((obj) => { ChangeDateCalendar((DateTime)obj); });
        private void ChangeDateCalendar(DateTime from)
        {
            var to = from.AddMonths(1).AddDays(-1);
            LoadSpecialDatesAsync(from, to);
        }

        public DelegateCommand<object> SignOutCommand => new DelegateCommand<object>(SelectedDate);
        private void SelectedDate(object date)
        {
            if (date == null)
                return;

            if (Dates.Any())
            {

            }
        }

        public TimesheetPageViewModel(IViewModelBaseModule baseModule, ITimesheetModule timesheetModule) : base(baseModule)
        {
            _timesheetModule = timesheetModule;

            Title = "Timesheet";
            IsBusy = true;

            DateTime date = DateTime.Now;
            _dates = new ObservableCollection<DateTime>();
            _specialDates = new ObservableCollection<SpecialDate>();

            var from = new DateTime(date.Year, date.Month, 1);
            var to = from.AddMonths(1).AddDays(-1);
            LoadSpecialDatesAsync(from, to);

        }

        private void LoadSpecialDatesAsync(DateTime from, DateTime to)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var timesheet = await _timesheetModule.TimesheetService.GetTimesheetBeetweenDates(from, to);
                    var specialDates = await _timesheetModule.CalendarService.GetSpecialDatesBeetweenDatesAsync(timesheet);
                    specialDates.ForEach(x => SpecialDates.Add(x));
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Booking] Error: {ex}");

                    await BaseModule.DialogService.ShowAlertAsync(
                        "An error ocurred, try again",
                        "Error",
                        "Ok");

                }

            });
        }

    }
}