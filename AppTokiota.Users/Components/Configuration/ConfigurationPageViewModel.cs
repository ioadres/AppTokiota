using System;
using System.Collections.Generic;
using System.Diagnostics;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Components.DashBoard;
using AppTokiota.Users.Components.Review;
using AppTokiota.Users.Components.Timesheet;
using Plugin.LocalNotifications;
using Prism.Commands;
using Xamarin.Forms;

namespace AppTokiota.Users.Components.Configuration
{
	public class ConfigurationPageViewModel: ViewModelBase
    {
        #region Services
		protected readonly IConfigurationModule _configurationModule;
        #endregion

        private bool _isEnableNotification;
        public bool IsEnableNotification
        {
            get { return _isEnableNotification; }
            set { 
                AppSettings.IsEnableNotification = value;
                SetProperty(ref _isEnableNotification, value); 

                if (AppSettings.IsEnableNotification)
                {
                    var dateNow = DateTime.Now;
                    var limitDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 16, 0, 0);
                    CrossLocalNotifications.Current.Show("Tokiota :: Timesheet", "Remember check your timesheet", 1, limitDate);
                } else {
                    CrossLocalNotifications.Current.Cancel(1);
                }
            }
        }

        private bool _isEnableCache;
        public bool IsEnableCache
        {
            get { return _isEnableCache; }
            set
            {
                AppSettings.IsEnableCache = value;
                SetProperty(ref _isEnableCache, value);
            }
        }

        private int _hoursText;
        public int HoursText {
            get { return _hoursText; }
            set {
                SetProperty(ref _hoursText, value);
            }
        }

        private string _startupViewText;
        public string StartupViewText
        {
            get { return _startupViewText; }
            set
            {
                SetProperty(ref _startupViewText, value);
            }
        }

		public ConfigurationPageViewModel(IViewModelBaseModule baseModule, IConfigurationModule configurationModule) : base(baseModule)
        {
			_configurationModule = configurationModule;

            Title = "Configuration";
            _isEnableNotification = AppSettings.IsEnableNotification;
            _isEnableCache = AppSettings.IsEnableCache;
            _hoursText = AppSettings.HoursDay;
            _startupViewText = AppSettings.StartupView;

            OpenCodeCommand = new DelegateCommand(OpenCode);
            OpenTwitterCommand = new DelegateCommand(OpenTwitter);
            OpenStartUpViewCommand = new DelegateCommand(OpenStartUpView);
            OpenHoursViewCommand = new DelegateCommand(OpenHoursView);
            ClearCacheCommand = new DelegateCommand(ClearCache);
        }

        public DelegateCommand ClearCacheCommand { get; set; }
        private void ClearCache()
        {
            BaseModule.CacheEntity.DeleteLocalAll();
            BaseModule.DialogService.ShowToast("Action remove cache is success!!");
        }

        public DelegateCommand OpenHoursViewCommand { get; set; }
        private async void OpenHoursView()
        {
            try
            {
                var list = new List<string>();
                for (var i = 1; i <= 24; i++)
                {
                    list.Add(i.ToString());
                }
                var result = await BaseModule.DialogService.SelectActionAsync("Select the Hours/Day", "Default Hours/Day", list);
                if (!result.Equals("Cancel"))
                {
                    var value = int.Parse(result);
                    AppSettings.HoursDay = value;
                    HoursText = value;
                }
            } catch(Exception ex) {
                Debug.WriteLine($"[ConfigurationPage] Error: {ex}");
            }
        }

        public DelegateCommand OpenStartUpViewCommand { get; set; }
        private async void OpenStartUpView()
        {
            try
            {
                var list = new List<string>();
                list.Add(PageRoutes.GetKey<DashBoardPage>());
                list.Add(PageRoutes.GetKey<TimesheetPage>());
                list.Add(PageRoutes.GetKey<ReviewPage>());

                var result = await BaseModule.DialogService.SelectActionAsync("Select the startup view", "Default Startup View", list);
                if (!result.Equals("Cancel"))
                {
                    AppSettings.StartupView = result;
                    StartupViewText = result;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"[ConfigurationPage] Error: {ex}");
            }
        }


        public DelegateCommand OpenCodeCommand { get; set; }
        private void OpenCode()
        {
            Device.OpenUri(new Uri(AppSettings.UrlCodeCompany));
        }

        public DelegateCommand OpenTwitterCommand { get; set; }
        private void OpenTwitter()
        {
            Device.OpenUri(new Uri(AppSettings.UrlTwitterCompany));
        }



    }
}
