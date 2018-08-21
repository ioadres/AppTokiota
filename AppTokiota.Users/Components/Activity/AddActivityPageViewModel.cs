using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Models;
using Prism.Navigation;
using Prism.Commands;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;

namespace AppTokiota.Users.Components.Activity
{
    public class AddActivityPageViewModel : ViewModelBase
    {
        #region Services
        protected readonly IAddActivityModule _addActivityModule;
        #endregion

        private Models.Imputed _context;
        public Models.Imputed Context
        {
            get { return _context; }
        }

        private bool _timeImputationEntryVisibility;
        public bool TimeImputationEntryVisibility
        {
            get { return _timeImputationEntryVisibility; }
            set
            {
                SetProperty(ref _timeImputationEntryVisibility, value);
                if (!string.IsNullOrEmpty(TimeSelectedImputation))
                {
                    TimeTitleImputationEntryVisibility = !_timeImputationEntryVisibility;
                }
            }
        }

        private bool _timeTitleImputationEntryVisibility;
        public bool TimeTitleImputationEntryVisibility
        {
            get { return _timeTitleImputationEntryVisibility; }
            set { SetProperty(ref _timeTitleImputationEntryVisibility, value); }
        }

        private string _timeSelectedImputation;
        public string TimeSelectedImputation
        {
            get { return _timeSelectedImputation; }
            set { SetProperty(ref _timeSelectedImputation, value); }
        }

        #region TimeImputationAction
        public DelegateCommand<Dictionary<string, string>> TimeImputationCommand => new DelegateCommand<Dictionary<string, string>>(TimeImputationAction);
        protected void TimeImputationAction(Dictionary<string, string> response)
        {
            
            Context.Consumed.Minute = float.Parse(response["Minute"]);
            Context.Consumed.Hour = float.Parse(response["Hour"]);
            TimeSelectedImputation = Context.Consumed.ToString();
            TimeTitleImputationEntryVisibility = true;
        }
        #endregion

        #region TimeImputedOpen
        public DelegateCommand TimeImputedOpenCommand => new DelegateCommand(TimeImputedOpen);
        protected void TimeImputedOpen()
        {
            TimeImputationEntryVisibility = !TimeImputationEntryVisibility;
            TimeTitleImputationEntryVisibility = false;
        }
        #endregion

        #region CloseAction
        public DelegateCommand ClosePopupCommand => new DelegateCommand(ClosePopup);
        protected void ClosePopup()
        {
            BaseModule.NavigationService.GoBackAsync();
        }
        #endregion

        #region NextAction
        public DelegateCommand NextCommand => new DelegateCommand(Next);
        protected async void Next()
        {
			if (IsInternetAndCloseModal())
			{
				var navigationParameters = new NavigationParameters();
				navigationParameters.Add(Imputed.Tag, Context);
				await BaseModule.NavigationService.NavigateAsync(PageRoutes.GetKey<AddActivityTimeDesviationPage>(), navigationParameters, false, false);
			}
        }
        #endregion


        public AddActivityPageViewModel(IViewModelBaseModule baseModule, IAddActivityModule addActivityModule) : base(baseModule)
        {
            _addActivityModule = addActivityModule;
            Title = "Select Consumed";
            TimeTitleImputationEntryVisibility = true;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
			if(parameters.ContainsKey(Imputed.Tag)) {
                _context = parameters.GetValue<Imputed>(Imputed.Tag);
                TimeSelectedImputation = Context.Consumed.ToString();
			}
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(Imputed.Tag))
            {
                _context = parameters.GetValue<Imputed>(Imputed.Tag);
                TimeSelectedImputation = Context.Consumed.ToString();
            }

        }
    }
}

