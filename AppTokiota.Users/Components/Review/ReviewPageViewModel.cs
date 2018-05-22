using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Controls;
using AppTokiota.Users.Models;
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
    public class Review
    {
        public int id { get; set; }
        public string project { get; set; }
        public string task { get; set; }
        public string description { get; set; }
        public Time schedule { get; set; }
        public double consumed { get; set; }
        public double deviate { get; set; }
        public Time imputation { get; set; }
        public Time deviation { get; set; }

    }

    public class ReviewPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region Services
        protected readonly IReviewModule _reviewModule;
        #endregion

        #region Datapicker
        public ObservableCollection<PickerItem> yearPicker;
        public ObservableCollection<PickerItem> monthPicker;

        public ObservableCollection<PickerItem> YearPicker
        {
            get { return yearPicker; }
            set { SetProperty(ref yearPicker, value); }
        }
        public ObservableCollection<PickerItem> MonthPicker
        {
            get { return monthPicker; }
            set { SetProperty(ref monthPicker, value); }
        }


        private PickerItem myYearPicker;
        private PickerItem myMonthPicker;

        public PickerItem MyYearPicker
        {
            get { return myYearPicker; }
            set { SetProperty(ref myYearPicker, value); }
        }
        public PickerItem MyMonthPicker
        {
            get { return myMonthPicker; }
            set { SetProperty(ref myMonthPicker, value); }
        }

        #endregion datapicker

        #region DataReview
        private ObservableCollection<TimesheetForDay> lstReview;
        public ObservableCollection<TimesheetForDay> LstReview
        {
            get { return lstReview; }
            set { SetProperty(ref lstReview, value); }
        }
        private Models.Review _currentReview;
        public Models.Review CurrentReview
        {
            get { return _currentReview; }
            //set { SetProperty(ref _currentReview, value); }
        }
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

        //Todo Sacar a settings
        DateTimeFormatInfo dtinfo = new CultureInfo("en").DateTimeFormat;

        #region Construct
        public ReviewPageViewModel(IViewModelBaseModule baseModule, IReviewModule reviewModule) : base(baseModule)
        {
            _reviewModule = reviewModule;
            

            Title = "Review";
            MyMonthPicker = new PickerItem();
            MyYearPicker = new PickerItem();

            YearPicker = new ObservableCollection<PickerItem>();
            MonthPicker = new ObservableCollection<PickerItem>();



            //lstReview = new ObservableCollection<TimesheetForDay>();
            LstReview = new ObservableCollection<TimesheetForDay>();
            //LstReview.Add(new Review { id = 1, project = "Proyecto1", task = "Task1", description = "description1", schedule = new Time { Hour = 2, Minute = 20 }, consumed = 50, deviate = 3, imputation = new Time { Hour = 2, Minute = 20 }, deviation = new Time { Hour = 2, Minute = 20 } });
            //LstReview.Add(new Review { id = 2, project = "Proyecto2", task = "Task2", description = "description2", schedule = new Time { Hour = 2, Minute = 20 }, consumed = 20, deviate = 5, imputation = new Time { Hour = 2, Minute = 20 }, deviation = new Time { Hour = 2, Minute = 20 } });
        }
        #endregion constructor

        #region LoadPickersListViewData
        
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            LoadDataAsync();
        }

        protected async void LoadDataAsync()
        {
            IsBusy = true;
            try
            {
                if (this.IsInternetAndCloseModal())
                {
                    await LoadDataPickerAsync();
                    LoadDataReviewAsync(MyYearPicker.Value, MyMonthPicker.Value);
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
                Debug.WriteLine($"[GetTimesheet] Error: {ex}");
            }
        }
        private async Task LoadDataPickerAsync()
        {
            DateTime MyDate = DateTime.Now;

            await Task.Run(() =>
            {
                for (int iyear = DateTime.Now.Year - 1; iyear <= (DateTime.Now.Year + 1); iyear++)
                {
                    YearPicker.Add(new PickerItem { Value = iyear, DisplayName = iyear.ToString() });
                }
                MyMonthPicker.Value = MyDate.Month;
                MyMonthPicker.DisplayName = dtinfo.GetMonthName(MyDate.Month);

                for (int imes = DateTime.MinValue.Month; imes < DateTime.MaxValue.Month + 1; imes++)
                {

                    MonthPicker.Add(new PickerItem { Value = imes, DisplayName = dtinfo.GetMonthName(imes) });
                }
                MyYearPicker.Value = MyDate.Year;
                MyYearPicker.DisplayName = MyDate.Year.ToString();
                //GetDefaultValues();
            });




            //await Task.FromResult(true);
        }
        private void GetDefaultValues()
        {
            DateTime MyDate = DateTime.Now;
            MyMonthPicker.Value = MyDate.Month;
            MyMonthPicker.DisplayName = dtinfo.GetMonthName(MyDate.Month);
            MyYearPicker.Value = MyDate.Year;
            MyYearPicker.DisplayName = MyDate.Year.ToString();
        }
        protected void LoadDataReviewAsync(int year, int month)
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (this.IsInternetAndCloseModal())
                    {
                        _currentReview = await _reviewModule.ReviewService.GetReview(year, month);
                        var lstReviewDates = await _reviewModule.TimeLineService.GetListTimesheetForDay(_currentReview);
                        lstReviewDates.ForEach(x => LstReview.Add(x));
                        IsBusy = false;
                    }
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
                    Debug.WriteLine($"[Review load data] Error: {ex}");
                }

            });
        }
        
        #endregion LoadPickersListViewData

    }
}
