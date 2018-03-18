using System;
using System.Collections.ObjectModel;
using System.Linq;
using AppTokiota.Components.Core;
using AppTokiota.Components.Core.Module;
using Prism.Commands;
using Prism.Navigation;

namespace AppTokiota.Components.Timesheet
{
    public class TimesheetPageViewModel: ViewModelBase
    {
        private readonly ITimesheetModule _menuModule;

        private ObservableCollection<DateTime> _dates;

        public ObservableCollection<DateTime> Dates
        {
            get { return _dates; }
            set { SetProperty(ref _dates, value); }
        }

        private DateTime _from;
        private DateTime _until;
        private bool _isNextEnabled;

        public DelegateCommand<object> SignOutCommand => new DelegateCommand<object>(SelectedDate);

        public TimesheetPageViewModel(INavigationService navigationService, ITimesheetModule menuModule) : base(navigationService)
        {
            _menuModule = menuModule;
            Title = "Timesheet";

            var today = DateTime.Today;

            _dates = new ObservableCollection<DateTime>
            {
                today
            };

            SelectedDate(today);

        }

        public DateTime From
        {
            get { return _from; }
            set { SetProperty(ref _from, value); }
        }

        public DateTime Until
        {
            get { return _until; }
            set { SetProperty(ref _until, value); }
        }

        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set { SetProperty(ref _isNextEnabled, value); }
        }

        private void SelectedDate(object date)
        {
            if (date == null)
                return;

            if (Dates.Any())
            {
                From = Dates.OrderBy(d => d.Day).FirstOrDefault();
                Until = Dates.OrderBy(d => d.Day).LastOrDefault();
                IsNextEnabled = Dates.Any() ? true : false;
            }
        }
    }
}