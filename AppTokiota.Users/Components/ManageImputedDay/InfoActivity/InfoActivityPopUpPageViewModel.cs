using System;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Models;
using Prism.Commands;
using Prism.Navigation;

namespace AppTokiota.Users.Components.ManageImputedDay
{
    public class InfoActivityPopUpPageViewModel: ViewModelBase
    {
        #region Services
        protected readonly IManageImputedDayModule _manageImputedDayModule;
        #endregion

        private double _deviation;
        public double Deviation
        {
            get { return _deviation; }
            set { SetProperty(ref _deviation, value); }
        }

        private double _consumed;
        public double Imputed
        {
            get { return _consumed; }
            set { SetProperty(ref _consumed, value); }
        }

        private Models.ActivityDay _context;
        public Models.ActivityDay Context
        {
            get { return _context; }
            set { SetProperty(ref _context, value); }
        }

        public InfoActivityPopUpPageViewModel(IViewModelBaseModule baseModule, IManageImputedDayModule manageImputedDayModule) : base(baseModule)
        {
            _manageImputedDayModule = manageImputedDayModule;

            Title = "Activity";
            Deviation = 0;
            Imputed = 0;
        }

        #region CloseAction
        public DelegateCommand ClosePopupCommand => new DelegateCommand(ClosePopup);
        protected void ClosePopup()
        {
            BaseModule.NavigationService.GoBackAsync();
        }
        #endregion


        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Context = parameters.GetValue<ActivityDay>(ActivityDay.Tag);
            Imputed = Context.Imputed;
            Deviation = Context.Deviation;           
        }

    }
}
