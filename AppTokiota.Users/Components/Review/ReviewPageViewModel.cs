using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Components.ManageImputedDay;
using AppTokiota.Users.Controls;
using AppTokiota.Users.Models;
using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTokiota.Users.Components.Review
{
    public class ReviewPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        DateTimeFormatInfo dtinfo = new CultureInfo(AppSettings.CultureInfoApp).DateTimeFormat;

        #region Services
        protected readonly IReviewModule _reviewModule;
        #endregion

        private IList<TimesheetForDay> _lstReviewDates;
        public IList<TimesheetForDay> LstReviewDates
        {
            get { return _lstReviewDates; }
            set { _lstReviewDates = value; }
        }
        #region Datapicker
        private ObservableCollection<PickerItem> _yearPicker;
        public ObservableCollection<PickerItem> YearPicker
        {
            get { return _yearPicker; }
            set { SetProperty(ref _yearPicker, value); }
        }

        private ObservableCollection<PickerItem> _monthPicker;
        public ObservableCollection<PickerItem> MonthPicker
        {
            get { return _monthPicker; }
            set { SetProperty(ref _monthPicker, value); }
        }

        private PickerItem _myItemYearPicker;

        public PickerItem MyItemYearPicker
        {
            get { return _myItemYearPicker; }
            set
            {
                if (value != _myItemYearPicker && value != null)
                {
                    SetProperty(ref _myItemYearPicker, value);
                    //LoadDataReviewByDate(value.Value, _myItemMonthPicker.Value);
                }
            }
        }
        private PickerItem _myItemMonthPicker;

        public PickerItem MyItemMonthPicker
        {
            get { return _myItemMonthPicker; }
            set
            {
                if (value != _myItemMonthPicker && value != null)
                {
                    SetProperty(ref _myItemMonthPicker, value);
                    //LoadDataReviewByDate();
                }
            }
        }

        #endregion datapicker

        #region DataReview
        private ObservableCollection<ReviewTimeLine> _lstReview;
        public ObservableCollection<ReviewTimeLine> LstReview
        {
            get { return _lstReview; }
            set { SetProperty(ref _lstReview, value); }
        }


        private Models.Review _currentReview;

        #endregion DataReview

        /// <summary>
        /// Gets or sets the Total DeviationTotal 
        /// </summary>
        /// <value>The Total DeviationTotal</value>
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

        public bool _btnSendReviewIsVisible;
        public bool BtnSendReviewIsVisible
        {
            get { return _btnSendReviewIsVisible; }
            set { SetProperty(ref _btnSendReviewIsVisible, value); }
        }

        #region Construct
        public ReviewPageViewModel(IViewModelBaseModule baseModule, IReviewModule reviewModule) : base(baseModule)
        {
            _reviewModule = reviewModule;
            _yearPicker = new ObservableCollection<PickerItem>();
            _monthPicker = new ObservableCollection<PickerItem>();

            Title = "Review";
            ModeLoadingPopUp = true;
            LstReview = new ObservableCollection<ReviewTimeLine>();
            LoadDataAsync();
        }
        #endregion constructor

        #region LoadPickersListViewData

        protected void LoadDataAsync()
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (this.IsInternetAndCloseModal())
                    {
                        await LoadDataPickerAsync();
                        LoadDataReviewByDate();
                    }
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
                    Crashes.TrackError(ex);
                }
            });

        }

        private async Task LoadDataPickerAsync()
        {
            await Task.Run(() =>
            {
				var yearPickerTemp = new ObservableCollection<PickerItem>();
                for (int iyear = DateTime.Now.Year - 1; iyear <= (DateTime.Now.Year + 1); iyear++)
                {
					yearPickerTemp.Add(new PickerItem { Value = iyear, DisplayName = iyear.ToString() });
                }
				YearPicker = yearPickerTemp;

				var monthPickerTemp = new ObservableCollection<PickerItem>();
                for (int imes = DateTime.MinValue.Month; imes < DateTime.MaxValue.Month + 1; imes++)
                {
					monthPickerTemp.Add(new PickerItem { Value = imes, DisplayName = dtinfo.GetMonthName(imes) });
                }
				MonthPicker = monthPickerTemp;
                LoadDefaultValues();
            });
            
        }

        private void LoadDefaultValues()
        {
            DateTime MyDate = DateTime.Now;
            var InitYearPickerItem = new PickerItem
            {
                Value = MyDate.Year,
                DisplayName = MyDate.Year.ToString(),
            };

            var InitMonthPickerItem = new PickerItem
            {
                Value = MyDate.Month,
                DisplayName = dtinfo.GetMonthName(MyDate.Month),
            };

            MyItemMonthPicker = MonthPicker.Where(x=>x.Value == InitMonthPickerItem.Value).FirstOrDefault();
            MyItemYearPicker = YearPicker.Where(x => x.Value == InitYearPickerItem.Value).FirstOrDefault();
        }

        protected void LoadDataReviewByDate()
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (this.IsInternetAndCloseModal())
                    {
                        _currentReview = await _reviewModule.ReviewService.GetReview(MyItemYearPicker.Value, MyItemMonthPicker.Value);
                        LoadDataReviewAsync(_currentReview);
                    }
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
                    Crashes.TrackError(ex);
                }
            });
        }

        protected async void LoadDataReviewAsync(Models.Review review)
        {
            try
            {
                BtnSendReviewIsVisible = !(review.IsValidated || review.IsClosed);
                LstReviewDates = await _reviewModule.TimeLineService.GetListTimesheetForDay(review);
                LoadTotalTime(LstReviewDates);
                var listTemp = new ObservableCollection<ReviewTimeLine>();
                LstReviewDates.ForEach(x => listTemp.Add(map(x)));
                listTemp.Last().IsLast = true;
                LstReview = listTemp;
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
                Crashes.TrackError(ex);
            }
        }

        private void LoadTotalTime(IList<TimesheetForDay> lstReviewDates)
        {
            foreach (var tsd in lstReviewDates)
            {
                ImputedTotal = ImputedTotal + tsd.Activities.Sum(x => x.Imputed);
                DeviationTotal = DeviationTotal + tsd.Activities.Sum(x => x.Deviation);
            }
        }

        private ReviewTimeLine map(TimesheetForDay x)
        {
            var currentTimeSheetDay = new ReviewTimeLine();
            currentTimeSheetDay.ProjectsForDay = x.Activities.Select(y => y.Project).Distinct().Count();
            currentTimeSheetDay.TasksForDay = x.Activities.Select(y => y.Task).Distinct().Count();
            currentTimeSheetDay.DesviationTasksDay = x.Activities.Sum(d => d.Deviation);
            currentTimeSheetDay.ImputationTasksDay = x.Activities.Sum(d => d.Imputed);
            currentTimeSheetDay.Day = x.Day;
            currentTimeSheetDay.IsLast = x.IsLast;
            return currentTimeSheetDay;
        }

        #endregion LoadPickersListViewData

        #region NavigateToManageImputedDay
        public DelegateCommand<object> ManageImputedDayCommand => new DelegateCommand<object>((obj)=> { ManageImputedDay((ReviewTimeLine)obj); });
        protected async void ManageImputedDay(ReviewTimeLine from)
        {
            if (this.IsInternetAndCloseModal())
            {
                try
                {
                    var selectedDateTimesheet = LstReviewDates.Where(x => x.Day == from.Day).FirstOrDefault();
                    if (selectedDateTimesheet.Day != null)
                    {
                        var navigationParameters = new NavigationParameters();
                        navigationParameters.Add(TimesheetForDay.Tag, selectedDateTimesheet);
                        navigationParameters.Add("IsVisibleButtonAdd", false);
                        await BaseModule.NavigationService.NavigateAsync(PageRoutes.GetKey<ManageImputedDayPage>(), navigationParameters);
                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }
                }
                catch (Exception e)
                {
                    BaseModule.DialogService.ShowToast("Fail load Detail. Try Again.");
                }
            }
        }
        #endregion

        public DelegateCommand LoadDataReviewByDateCommand => new DelegateCommand(LoadDataReviewByDate);


        #region sendValidateReview

        public DelegateCommand SendReviewValidateCommand => new DelegateCommand(SendReviewToValidate);

        protected void SendReviewToValidate()
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (this.IsInternetAndCloseModal())
                    {
                        var response = await _reviewModule.ReviewService.PatchReview(MyItemYearPicker.Value, MyItemMonthPicker.Value);
                        if (response)
                        {
                            LoadDataReviewByDate();
                        }
                        else
                        {
                            BaseModule.DialogService.ShowToast("The sending review is not avaible in this moment. Please try again later.");
                        }
                        IsBusy = false;
                    }
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
                    Crashes.TrackError(ex);
                }

            });
        }
        #endregion
    }
}
