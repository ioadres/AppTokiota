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
        #region Services
        protected readonly ITimesheetModule _timesheetModule;
        #endregion

        #region Construct
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
        #endregion

        /// <summary>
        /// Gets or sets the selection collection dates 
        /// </summary>
        /// <value>The special dates</value>
        private ObservableCollection<DateTime> _dates;
        public ObservableCollection<DateTime> Dates
        {
            get { return _dates; }
            set { SetProperty(ref _dates, value); }
        }

        /// <summary>
        /// Gets or sets the special collection dates 
        /// </summary>
        /// <value>The special dates</value>
        private ObservableCollection<SpecialDate> _specialDates;
        public ObservableCollection<SpecialDate> SpecialDates
        {
            get { return _specialDates; }
            set { SetProperty(ref _specialDates, value); }
        }

        #region EventChangeDateMonthOfCalendar
        public Command DateChosen => new Command((obj) => { ChangeDateCalendar((DateTime)obj); });
        protected void ChangeDateCalendar(DateTime from)
        {
            var to = from.AddMonths(1).AddDays(-1);
            LoadSpecialDatesAsync(from, to);
        }
        #endregion

        #region EventToDetectChangeInTheSelectedDate
        public DelegateCommand<object> SelectedDateCommand => new DelegateCommand<object>(SelectedDate);
        protected void SelectedDate(object date)
        {
            if (date == null)
                return;

            if (Dates.Any())
            {

            }
        }
        #endregion

        #region MethodToLoadSpecialDatesFromTheSelectionMonthInCalendar
        protected void LoadSpecialDatesAsync(DateTime from, DateTime to)
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
        #endregion
    }
}