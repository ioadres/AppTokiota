using System;
using System.Threading.Tasks;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Components.Login;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace AppTokiota.Users.Components.Core
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public IViewModelBaseModule BaseModule { get; set; }
        public DelegateCommand<string> NavigateCommand { get; set; }

        public ViewModelBase(IViewModelBaseModule baseModule)
        {
            BaseModule = baseModule;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            Title = string.Empty;
        }

        private async void Navigate(string name)
        {
            await BaseModule.NavigationService.NavigateAsync(name);
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
