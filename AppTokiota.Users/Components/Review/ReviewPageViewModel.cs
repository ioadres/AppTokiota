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

    public class ReviewPageViewModel :ViewModelBase, INotifyPropertyChanged
    {
        #region Services
        protected readonly IReviewModule _reviewModule;
        #endregion

        #region Datapicker
        public ObservableCollection<object> yearPicker { get; set; }
        public ObservableCollection<object> monthPicker { get; set; }

        public ObservableCollection<object> YearPicker
        {
            get { return yearPicker; }
            set { yearPicker = value; RaisePropertyChanged("Selected"); }
        }
        public ObservableCollection<object> MonthPicker
        {
            get { return monthPicker; }
            set { monthPicker = value; RaisePropertyChanged("Selected"); }
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
       

        private ObservableCollection<Review> lstReview;
        public ObservableCollection<Review> LstReview
        {
            get { return lstReview; }
            set { SetProperty(ref lstReview, value); }
        }

        //Todo Sacar a settings
        DateTimeFormatInfo dtinfo = new CultureInfo("en").DateTimeFormat;

        #region Construct
        public ReviewPageViewModel(IViewModelBaseModule baseModule, IReviewModule reviewModule) : base(baseModule)
        {
            _reviewModule = reviewModule;

            Title = "Review";

            DateTime MyDate = DateTime.Now;

            MonthPicker = new ObservableCollection<object>();
            YearPicker = new ObservableCollection<object>();

            LoadDataPicker();

            //LstReview = new ObservableCollection<Review>();
            //LstReview.Add(new Review { id = 1, project = "Proyecto1", task = "Task1", description = "description1", schedule=new Time {Hour= 2, Minute = 20 },consumed=50,deviate= 3, imputation = new Time { Hour = 2, Minute = 20 }, deviation = new Time { Hour = 2, Minute = 20 } });
            //LstReview.Add(new Review { id = 2, project = "Proyecto2", task = "Task2", description = "description2", schedule = new Time { Hour = 2, Minute = 20 }, consumed=20,  deviate=5, imputation = new Time { Hour = 2, Minute = 20 }, deviation = new Time { Hour = 2, Minute = 20 } });
        }

        private async void LoadDataPicker()
        {
            for (int iyear = DateTime.Now.Year - 1; iyear <= (DateTime.Now.Year + 1); iyear++)
            {
                YearPicker.Add(iyear);
            }

            for (int imes = DateTime.MinValue.Month; imes < DateTime.MaxValue.Month + 1; imes++)
            {

                MonthPicker.Add(dtinfo.GetMonthName(imes));
            }
            await Task.FromResult(true);
        }

        #endregion constructor

        private Models.Review _currentReview;

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

        //public void RaisePropertyChanged(string name)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(name));
        //}

    }
}
