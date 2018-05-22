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
        public ObservableCollection<int> yearPicker;
        public ObservableCollection<string> monthPicker;

        public ObservableCollection<int> YearPicker
        {
            get { return yearPicker; }
            set { SetProperty(ref yearPicker, value); }
        }
        public ObservableCollection<string> MonthPicker
        {
            get { return monthPicker; }
            set { SetProperty(ref monthPicker, value); }
        }


        private int myIndexYearPicker;
        private int myIndexMonthPicker;

        public int MyIndexYearPicker
        {
            get { return myIndexYearPicker; }
            set { SetProperty(ref myIndexYearPicker, value); }
        }
        public int MyIndexMonthPicker
        {
            get { return myIndexMonthPicker; }
            set { SetProperty(ref myIndexMonthPicker, value); }
        }

        #endregion datapicker



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
            YearPicker = new ObservableCollection<int>();
            MonthPicker = new ObservableCollection<string>();

            LstReview = new ObservableCollection<Review>();
            LstReview.Add(new Review { id = 1, project = "Proyecto1", task = "Task1", description = "description1", schedule = new Time { Hour = 2, Minute = 20 }, consumed = 50, deviate = 3, imputation = new Time { Hour = 2, Minute = 20 }, deviation = new Time { Hour = 2, Minute = 20 } });
            LstReview.Add(new Review { id = 2, project = "Proyecto2", task = "Task2", description = "description2", schedule = new Time { Hour = 2, Minute = 20 }, consumed = 20, deviate = 5, imputation = new Time { Hour = 2, Minute = 20 }, deviation = new Time { Hour = 2, Minute = 20 } });
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            LoadDataPicker();
            //LoadDataReview(YearPicker.ElementAt(MyIndexYearPicker), MonthPicker.ElementAt(MyIndexMonthPicker)); 
            LoadDataReview(2018, 05);
        }

        private async void LoadDataPicker()
        {
            DateTime MyDate = DateTime.Now;
            for (int iyear = DateTime.Now.Year - 1; iyear <= (DateTime.Now.Year + 1); iyear++)
            {
                YearPicker.Add(iyear);
            }

            for (int imes = DateTime.MinValue.Month; imes < DateTime.MaxValue.Month + 1; imes++)
            {

                MonthPicker.Add(dtinfo.GetMonthName(imes));
            }

            MyIndexMonthPicker = MonthPicker.IndexOf(dtinfo.GetMonthName(MyDate.Month));
            MyIndexYearPicker = YearPicker.IndexOf(MyDate.Year);

            await Task.FromResult(true);
        }

        #endregion constructor

        //private Models.Review _currentReview;

        private ObservableCollection<Review> lstReview;
        public ObservableCollection<Review> LstReview
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

        #region LoadDataReviewFromDatesPicker
        protected void LoadDataReview(int year, int month)
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (this.IsInternetAndCloseModal())
                    {
                        _currentReview = await _reviewModule.ReviewService.GetReview(year, month);
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
        #endregion LoadDataReviewFromDatesPicker

    }
}
