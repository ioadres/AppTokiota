using System;
using System.Threading.Tasks;
using AppTokiota.Users.Components.Connection;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Components.Login;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace AppTokiota.Users.Components.Core
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { 
				SetProperty(ref _isBusy, value);
				if(value  && ModeLoadingPopUp) {
                    Device.BeginInvokeOnMainThread(() => BaseModule.DialogService.ShowLoading());
                }
                else {
					Device.BeginInvokeOnMainThread(() => BaseModule.DialogService.HideLoading());
                }

            }
        }

		private bool _modeLoadingPopUp = true;
		public bool ModeLoadingPopUp
        {
			get { return _modeLoadingPopUp; }
            set
            {
				_modeLoadingPopUp = value;
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public IViewModelBaseModule BaseModule { get; set; }
        public DelegateCommand<string> NavigateCommand { get; set; }
		public DelegateCommand RefreshCommand { get; set; }

        public ViewModelBase(IViewModelBaseModule baseModule)
        {
            BaseModule = baseModule;
            NavigateCommand = new DelegateCommand<string>(Navigate);
			RefreshCommand = new DelegateCommand(Refresh);
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

		public virtual void Refresh() {
			
		}
        
		public virtual bool IsInternetWithModal()
        {
			if (!IsInternet())
            {
                Task.Run(async () => {
                    await BaseModule.NavigationService.NavigateAsync(PageRoutes.GetKey<ConnectionPage>(), null, true, true);
                });
                Task.WaitAll();

				return false;
            }
			return true;
        }

		public virtual bool IsInternet()
        {
			return BaseModule.NetworkConnectionService.IsAvailable();
        }
    }
}
