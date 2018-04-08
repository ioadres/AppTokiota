using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Models;
using Prism.Navigation;

namespace AppTokiota.Users.Components.Activity
{
    public class AddActivityPageViewModel : ViewModelBase
    {
        #region Services
        protected readonly IAddActivityModule _addActivityModule;
        #endregion

        private Models.TimesheetForDay _currentTimesheetForDay;
        public Models.TimesheetForDay CurrentTimesheetForDay
        {
            get { return _currentTimesheetForDay; }
        }

        public AddActivityPageViewModel(IViewModelBaseModule baseModule, IAddActivityModule addActivityModule) : base(baseModule)
        {
           _addActivityModule = addActivityModule;

            Title = "Add Activity";
        }    

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            _currentTimesheetForDay = parameters.GetValue<TimesheetForDay>(TimesheetForDay.Tag);
        }

		public override void OnNavigatedFrom(NavigationParameters parameters)
		{
           
		}
	}
}

