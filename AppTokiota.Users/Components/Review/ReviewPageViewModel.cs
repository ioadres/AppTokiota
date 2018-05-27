﻿using AppTokiota.Users.Components.Core;
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
    public class ReviewTimeLine
    {
        public Day Day { get; set; }
        public bool IsLast { get; set; } = false;
        public ActivityDay Activity { get; set; }
    }
    public class ReviewPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        //Todo Sacar a settings
        DateTimeFormatInfo dtinfo = new CultureInfo("en").DateTimeFormat;

        #region Services
        protected readonly IReviewModule _reviewModule;
        #endregion

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


        private int _myIndexYearPicker;
        public int MyIndexYearPicker
        {
            get { return _myIndexYearPicker; }
            set { SetProperty(ref _myIndexYearPicker, value); }
        }
        
        private int _myIndexMonthPicker;
        public int MyIndexMonthPicker
        {
            get { return _myIndexMonthPicker; }
            set { SetProperty(ref _myIndexMonthPicker, value); }
        }

        #endregion datapicker

        #region DataReview
        //private ObservableCollection<TimesheetForDay> _lstReview;
        //public ObservableCollection<TimesheetForDay> LstReview
        //{
        //    get { return _lstReview; }
        //    set { SetProperty(ref _lstReview, value); }
        //}

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

        #region Construct
        public ReviewPageViewModel(IViewModelBaseModule baseModule, IReviewModule reviewModule) : base(baseModule)
        {
            _reviewModule = reviewModule;           

            Title = "Review";
            _yearPicker = new ObservableCollection<PickerItem>();
            _monthPicker = new ObservableCollection<PickerItem>();
            
                _lstReview = new ObservableCollection<ReviewTimeLine>();
            //_lstReview = new ObservableCollection<TimesheetForDay>();
        }
        #endregion constructor

        #region LoadPickersListViewData
        
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            LoadDataAsync();
        }

        protected void LoadDataAsync()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsBusy = true;
                try
                {
                    if (this.IsInternetAndCloseModal())
                    {
                        await LoadDataPickerAsync();
                        LoadDataReviewAsync(YearPicker.ElementAt(MyIndexYearPicker).Value,MonthPicker.ElementAt(MyIndexMonthPicker).Value);
                    }
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
                    Debug.WriteLine($"[GetTimesheet] Error: {ex}");
                }
            });
        }


        private async Task LoadDataPickerAsync()
        {
            await Task.Run(() =>
            {
                for (int iyear = DateTime.Now.Year - 1; iyear <= (DateTime.Now.Year + 1); iyear++)
                {
                    YearPicker.Add(new PickerItem { Value = iyear, DisplayName = iyear.ToString() });
                }

                for (int imes = DateTime.MinValue.Month; imes < DateTime.MaxValue.Month + 1; imes++)
                {

                    MonthPicker.Add(new PickerItem { Value = imes, DisplayName = dtinfo.GetMonthName(imes) });
                }
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
            } ;

            var InitMonthPickerItem = new PickerItem
            {
                Value = MyDate.Month,
                DisplayName = dtinfo.GetMonthName(MyDate.Month),
            };

            MyIndexYearPicker = YearPicker.IndexOf(YearPicker.Where(x=>x.Value == InitYearPickerItem.Value).FirstOrDefault());
            MyIndexMonthPicker = MonthPicker.IndexOf(MonthPicker.Where(x=>x.Value == InitMonthPickerItem.Value).FirstOrDefault());
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
                        lstReviewDates.ForEach(x => LstReview.Add(map(x)));
                        LstReview.Last().IsLast = true;
                        //var a = LstReview;
                        //var b = LstReview[1].Activities[0].Description;
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

        private ReviewTimeLine map(TimesheetForDay x)
        {
            var currentTimeSheetDay = new ReviewTimeLine();
            currentTimeSheetDay.Activity = x.Activities.FirstOrDefault();
            currentTimeSheetDay.Day = x.Day;
            currentTimeSheetDay.IsLast = x.IsLast; 
            return currentTimeSheetDay; 

        }

        #endregion LoadPickersListViewData

    }
}
