using System;
using System.Collections.ObjectModel;
using System.Linq;
using AppTokiota.Components.Core;
using AppTokiota.Components.Core.Module;
using Prism.Commands;
using Prism.Navigation;
using AppTokiota.Controls;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

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

        public TimesheetPageViewModel(IViewModelBaseModule baseModule, ITimesheetModule timesheetModule) : base(baseModule)
        {
            _timesheetModule = timesheetModule;

            Title = "Timesheet";
            IsBusy = true;

            var today = DateTime.Today;

            _dates = new ObservableCollection<DateTime>
            {
                today
            };

            _specialDates = new ObservableCollection<SpecialDate>();

            LoadSpecialDatesAsync();
            SelectedDate(today);

        }

        private void LoadSpecialDatesAsync()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    DateTime date = DateTime.Now;
                    var from = new DateTime(date.Year, date.Month, 1);
                    var to = from.AddMonths(1).AddDays(-1);
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