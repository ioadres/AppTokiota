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
using Plugin.DeviceOrientation;
using AppTokiota.Users.Components.ManageImputedDay;
using AppTokiota.Users.Models;

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

            DateTime date = DateTime.Now;
            _dates = new ObservableCollection<DateTime>();
            _specialDates = new ObservableCollection<SpecialDate>();

            var from = new DateTime(date.Year, date.Month, 1);
            DateChosen.Execute(from);

            _orientation = (int)CrossDeviceOrientation.Current.CurrentOrientation == 1 ||  (int)CrossDeviceOrientation.Current.CurrentOrientation == 4? StackOrientation.Horizontal : StackOrientation.Vertical;
            CrossDeviceOrientation.Current.OrientationChanged += (sender, args) =>
            {
                Orientation = (int)CrossDeviceOrientation.Current.CurrentOrientation == 1 || (int)CrossDeviceOrientation.Current.CurrentOrientation == 4 ? StackOrientation.Horizontal : StackOrientation.Vertical;
            };

        }
        #endregion

        private Models.Timesheet _currentTimesheet;

        /// <summary>
        /// Gets or sets the selection collection dates 
        /// </summary>
        /// <value>The special dates</value>
        private ObservableCollection<DateTime> _dates;
        public ObservableCollection<DateTime> Dates
        {
            get { return _dates; }
            set { 
                SetProperty(ref _dates, value);
            }
        }

        /// <summary>
        /// Gets if is a multipleSelection
        /// </summary>
        /// <value>If is multile</value>
        private StackOrientation _orientation = StackOrientation.Vertical;
        public StackOrientation Orientation
        {
            get { return _orientation; }
            set { SetProperty(ref _orientation, value); }
        }

        /// <summary>
        /// Gets if is a multipleSelection
        /// </summary>
        /// <value>If is multile</value>
        private bool _isMultiple;
        public bool IsMultiple
        {
            get { return Dates.Count() > 1; }
            set { SetProperty(ref _isMultiple, value); }
        }

        /// <summary>
        /// Gets if is not a multiple selection
        /// </summary>
        /// <value>If is not multile</value>
        private bool _isNotMultiple;
        public bool IsNotMultiple
        {
            get { return Dates.Count() == 1; }
            set { SetProperty(ref _isNotMultiple, value); }
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
            var to = from.AddMonths(1).AddDays(10);
            LoadSpecialDatesAsync(from.AddDays(-7), to);
        }
        #endregion

        #region EventToDetectChangeInTheSelectedDate
        public DelegateCommand<object> SelectedDateCommand => new DelegateCommand<object>(SelectedDate);
        protected void SelectedDate(object date)
        {
            if (date == null)
                return;

            IsMultiple = Dates.Count() > 1;
            IsNotMultiple = Dates.Count() == 1;

            if (Dates.Any())
            {
            }
        }
        #endregion

        #region NavigateToManageImputedDay
        public DelegateCommand ManageImputedDayCommand => new DelegateCommand(ManageImputedDay);
        protected async void ManageImputedDay()
        {
            var selectedDateTimesheet = _timesheetModule.TimesheetService.GetTimesheetByDate(_currentTimesheet, Dates.FirstOrDefault());
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(TimesheetForDay.Tag, selectedDateTimesheet);
            await BaseModule.NavigationService.NavigateAsync(ManageImputedDayModule.Tag, navigationParameters);
        }
        #endregion

        #region MethodToLoadSpecialDatesFromTheSelectionMonthInCalendar
        protected void LoadSpecialDatesAsync(DateTime from, DateTime to)
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    Dates.Clear();
                    _currentTimesheet = await _timesheetModule.TimesheetService.GetTimesheetBeetweenDates(from, to);
                    var specialDates = await _timesheetModule.CalendarService.GetSpecialDatesBeetweenDatesAsync(_currentTimesheet);
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