using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Controls;
using AppTokiota.Users.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppTokiota.Users.Components.Review
{
    public class Contacto
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        
    }
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
            var lstContatos = new List<Contacto>();
            lstContatos.Add(new Contacto
            { id = 1,userId = 1, body="Descripcion1",title= "Titulo1"
            });
            lstContatos.Add(new Contacto
            {
                id = 2,
                userId = 2,
                body = "Descripcion2",
                title = "Titulo2"
            });

        }
        private Models.TimesheetForDay _currentTimesheetForDay;
        public Models.TimesheetForDay CurrentTimesheetForDay
        {
            get { return _currentTimesheetForDay; }
        }
        #endregion
        public ObservableCollection<Contacto> Contatos { get; }
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

        /// <summary>
        /// Gets or sets the activities collection dates 
        /// </summary>
        /// <value>The activities</value>
        private ObservableCollection<ActivityDay> _activities;
        public ObservableCollection<ActivityDay> Activities
        {
            get { return _activities; }
            set { SetProperty(ref _activities, value); }
        }

        /// <summary>
        /// Gets or sets the Total Deviation 
        /// </summary>
        /// <value>The Total Deviation</value>
        private double _deviation;
        public double Deviation
        {
            get { return _deviation; }
            set { SetProperty(ref _deviation, value); }
        }

        /// <summary>
        /// Gets or sets the Imputed 
        /// </summary>
        /// <value>The Imputed</value>
        private double _imputed;
        public double Imputed
        {
            get { return _imputed; }
            set { SetProperty(ref _imputed, value); }
        }
        



        public DelegateCommand<object> OnInfoActivityItemCommand => new DelegateCommand<object>((obj) => { OnInfoActivityItem((ActivityDay)obj); });

        protected void OnInfoActivityItem(ActivityDay from)
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(ActivityDay.Tag, from);
            BaseModule.NavigationService.NavigateAsync(PageRoutes.GetKey<Timesheet.TimesheetPage>(), navigationParameters, true, true);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var keyContains = parameters.ContainsKey(TimesheetForDay.Tag);
            if (keyContains)
            {
                var currentTimesheetForDay = parameters.GetValue<TimesheetForDay>(TimesheetForDay.Tag);
                GetMonthOfTimeSheet(currentTimesheetForDay);
                Title = currentTimesheetForDay.Day.Date.ToString("yyyy-MM-dd");
            }
        }

        private void GetMonthOfTimeSheet(TimesheetForDay timesheet)
        {
            _currentTimesheetForDay = timesheet;

            Device.BeginInvokeOnMainThread(() =>
            {
                Activities = new ObservableCollection<ActivityDay>(timesheet.Activities);
                Imputed = Activities.Sum(x => x.Imputed);
                Deviation = Activities.Sum(x => x.Deviation);
            });
        }

    }
}
