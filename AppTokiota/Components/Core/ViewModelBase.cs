using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace AppTokiota.Components.Core
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        protected readonly INavigationService _navigationService;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            Title = string.Empty;
        }

        private async void Navigate(string name)
        {
            await _navigationService.NavigateAsync(name);
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
