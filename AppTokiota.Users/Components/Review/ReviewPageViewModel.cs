using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppTokiota.Users.Components.Review
{
    public class ReviewPageViewModel :ViewModelBase
    {
        #region Services
        protected readonly IReviewModule _reviewModule;
        #endregion

        #region Construct
        public ReviewPageViewModel(IViewModelBaseModule baseModule, IReviewModule reviewModule) : base(baseModule)
        {
            _reviewModule = reviewModule;

            Title = "Review";

            DateTime date = DateTime.Now;
            _dates = new ObservableCollection<DateTime>();
            _specialDates = new ObservableCollection<SpecialDate>();

            var from = new DateTime(date.Year, date.Month, 1);
            //DateChosen.Execute(from);

        }
        #endregion

        private ObservableCollection<DateTime> _dates;
        public ObservableCollection<DateTime> Dates
        {
            get { return _dates; }
            set
            {
                SetProperty(ref _dates, value);
            }
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
    }
}
