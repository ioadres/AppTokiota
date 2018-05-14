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
using AppTokiota.Users.Components;
using AppTokiota.Users.Models;
using AppTokiota.Users.Components.ManageImputedDay;
using AppTokiota.Users.Components.Activity;

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
			ModeLoadingPopUp = false;

            Title = "Timesheet";

            DateTime date = DateTime.Now;
            _dates = new ObservableCollection<DateTime>();
            _specialDates = new ObservableCollection<SpecialDate>();

			_currentDayMonthYear = new DateTime(date.Year, date.Month, 1);
        }
        #endregion

        private Models.Timesheet _currentTimesheet;
		private DateTime _currentDayMonthYear;

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
        private bool _isMultiple;
        public bool IsMultiple
        {
            get { return _isMultiple; }
            set { SetProperty(ref _isMultiple, value); }
        }

        /// <summary>
        /// Gets if is not a multiple selection
        /// </summary>
        /// <value>If is not multiple selection</value>
        private bool _isNotMultiple;
        public bool IsNotMultiple
        {
            get { return _isNotMultiple; }
            set { SetProperty(ref _isNotMultiple, value); }
        }

        /// <summary>
        /// Get if  Date no selected 
        /// </summary>
        /// <value></value>
        private bool _isVisibleFooter;
        public bool IsVisibleFooter
        {
            get { return _isVisibleFooter; }
            set { SetProperty(ref _isVisibleFooter, value); }
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


        /// <summary>
        /// Gets or sets the Total Deviation 
        /// </summary>
        /// <value>The Total Deviation</value>
        private double _deviationTotal;
        public double DeviationTotal
        {
            get { return _deviationTotal; }
            set { SetProperty(ref _deviationTotal, value); }
        }

        /// <summary>
        /// Gets or sets the Total Imputed 
        /// </summary>
        /// <value>The Total Imputed</value>
        private double _imputedTotal;
        public double ImputedTotal
        {
            get { return _imputedTotal; }
            set { SetProperty(ref _imputedTotal, value); }
        }

        #region EventChangeDateMonthOfCalendar
        public Command DateChosen => new Command((obj) => { ChangeDateCalendar((DateTime)obj); });
        protected void ChangeDateCalendar(DateTime from, bool removeOldDates = true)
        {
			if (removeOldDates)
			{
				var itemsDate = Dates.ToList();
				itemsDate.ForEach(x => Dates.Remove(x));
				itemsDate = null;
			}

			ReloadData();

			_currentDayMonthYear = from;
            var to = from.AddMonths(1).AddDays(10);
            LoadSpecialDatesAsync(from.AddDays(-7), to);
        }
		#endregion

		#region EventReloadDataCalendar
		public DelegateCommand ReloadDataCalendarCommand => new DelegateCommand(ReloadDataCalendar);
		protected void ReloadDataCalendar()
        {
			ChangeDateCalendar(_currentDayMonthYear, false);
        }
        #endregion

        #region EventToDetectChangeInTheSelectedDate
        public DelegateCommand<object> SelectedDateCommand => new DelegateCommand<object>(SelectedDate);
        protected void SelectedDate(object date)
        {
            if (date == null)
                return;

			ReloadData();
        }
        #endregion

        #region NavigateToManageImputedDay
        public DelegateCommand ManageImputedDayCommand => new DelegateCommand(ManageImputedDay);
        protected async void ManageImputedDay()
        {
			if (this.IsInternetAndCloseModal())
			{
				var selectedDateTimesheet = _timesheetModule.TimesheetService.GetTimesheetByDate(_currentTimesheet, Dates.FirstOrDefault());
				if (selectedDateTimesheet.Day != null)
				{
					var navigationParameters = new NavigationParameters();
					navigationParameters.Add(TimesheetForDay.Tag, selectedDateTimesheet);
					await BaseModule.NavigationService.NavigateAsync(PageRoutes.GetKey<ManageImputedDayPage>(), navigationParameters);
				} else {
					BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
					ChangeDateCalendar(_currentDayMonthYear, false);
                }
			}
        }
        #endregion

        #region NavigateToManageImputedDay
        public DelegateCommand ManageMultipleImputedDayCommand => new DelegateCommand(ManageMultipleImputedDay);
        protected async void ManageMultipleImputedDay()
        {
			if (this.IsInternetAndCloseModal())
			{
				var selectedDateTimesheet = _timesheetModule.TimesheetService.GetTimesheetByDates(_currentTimesheet, Dates.ToList());

				if (selectedDateTimesheet.Days.Any())
				{
					var imputed = new Imputed()
					{
						CurrentTimesheetMultipleDay = selectedDateTimesheet
					};

					var navigationParameters = new NavigationParameters();
					navigationParameters.Add(Imputed.Tag, imputed);
					await BaseModule.NavigationService.NavigateAsync(PageRoutes.GetKey<AddActivityPage>(), navigationParameters);
				}
				else
				{
					BaseModule.DialogService.ShowToast("The all days selected is closed or failed load the month. The month will be load again");
					ChangeDateCalendar(_currentDayMonthYear, false);
				}
			}      
        }
        #endregion

        #region MethodToLoadSpecialDatesFromTheSelectionMonthInCalendar
        protected void LoadSpecialDatesAsync(DateTime from, DateTime to)
        {
			IsBusy = true;
			Device.BeginInvokeOnMainThread( async() =>
			{
				try
				{                    
					if (this.IsInternetAndCloseModal())
					{
						_currentTimesheet = await _timesheetModule.TimesheetService.GetTimesheetBeetweenDates(from, to);
						var specialDates = await _timesheetModule.CalendarService.GetSpecialDatesBeetweenDatesAsync(_currentTimesheet);
						specialDates.ForEach(x => SpecialDates.Add(x));
						IsBusy = false;
					}
				}
				catch (Exception ex)
				{
					IsBusy = false;
					BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();               
					Debug.WriteLine($"[Booking] Error: {ex}");
				}

			});            
            
        }
		#endregion

		      
		public override void OnNavigatedTo(NavigationParameters parameters)
        {
			if(_currentDayMonthYear != null) {
				ChangeDateCalendar(_currentDayMonthYear, false);
			}
        }


		private void ReloadData() {
			IsMultiple = Dates.Count() > 1;
            IsNotMultiple = Dates.Count() == 1;
            IsVisibleFooter = Dates.Count() == 0;

            if (Dates.Any())
            {
            }
		}

	}
}