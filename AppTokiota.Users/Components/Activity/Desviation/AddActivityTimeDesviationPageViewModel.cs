﻿using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Models;
using Prism.Commands;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTokiota.Users.Components.Activity
{
    public class AddActivityTimeDesviationPageViewModel : ViewModelBase
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

        private bool _timeDesviationEntryVisibility;
        public bool TimeDesviationEntryVisibility
        {
            get { return _timeDesviationEntryVisibility; }
            set { SetProperty(ref _timeDesviationEntryVisibility, value); }
        }

        private string _timeSelectedImputation;
        public string TimeSelectedImputation
        {
            get { return _timeSelectedImputation; }
            set { SetProperty(ref _timeSelectedImputation, value); }
        }

        private string _timeSelectedDesviation;
        public string TimeSelectedDesviation
        {
            get { return _timeSelectedDesviation; }
            set { SetProperty(ref _timeSelectedDesviation, value); }
        }


        #region TimeImputationAction
        public DelegateCommand<Dictionary<string, string>> TimeImputationCommand => new DelegateCommand<Dictionary<string, string>>(TimeImputationAction);
        protected void TimeImputationAction(Dictionary<string, string> response)
        {
            Context.Desviation.Minute = float.Parse(response["Minute"]);
            Context.Desviation.Hour = float.Parse(response["Hour"]);
            TimeSelectedImputation = Context.Desviation.ToString();
            TimeTitleImputationEntryVisibility = true;
        }
        #endregion

        #region TimeImputedOpen
        public DelegateCommand TimeImputedOpenCommand => new DelegateCommand(TimeImputedOpen);
        protected void TimeImputedOpen()
        {

            TimeTitleImputationEntryVisibility = false;
            TimeImputationEntryVisibility = !TimeImputationEntryVisibility;
        }
        #endregion

        #region GoBack
        public DelegateCommand GoBackCommand => new DelegateCommand(GoBack);
        protected void GoBack()
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(Imputed.Tag, Context);
            BaseModule.NavigationService.NavigateAsync($"../{PageRoutes.GetKey<AddActivityPage>()}",navigationParameters,false, false);
        }
        #endregion

        #region CloseAction
        public DelegateCommand CloseCommand => new DelegateCommand(Close);
        protected void Close()
        {
            BaseModule.NavigationService.GoBackAsync();
        }
        #endregion

        #region NextAction
        public DelegateCommand NextCommand => new DelegateCommand(Next);
        protected async void Next()
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(Imputed.Tag, Context);
            await BaseModule.NavigationService.NavigateAsync(PageRoutes.GetKey<AddActivityProjectPage>(), navigationParameters, false, false);
        }

        #endregion

        public AddActivityTimeDesviationPageViewModel(IViewModelBaseModule baseModule, IAddActivityModule addActivityModule) : base(baseModule)
        {
            _addActivityModule = addActivityModule;
            Title = "Select Desviation";
            TimeTitleImputationEntryVisibility = true;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            _context = parameters.GetValue<Imputed>(Imputed.Tag);
            TimeSelectedImputation = Context.Desviation.ToString();
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            //_currentTimesheetForDay = parameters.GetValue<TimesheetForDay>(TimesheetForDay.Tag);
        }
    }
}
