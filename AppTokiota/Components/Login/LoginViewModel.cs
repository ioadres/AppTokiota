using System;
using AppTokiota.Attributes;
using AppTokiota.Components.Core;
using AppTokiota.Components.Core.Interfaces;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AppTokiota.Components.Login
{
    public class LoginViewModel : ViewModelBase
    {

        private BaseLoginModule _loginModule { get; set; }

        public LoginViewModel(INavigationService navigationService, BaseLoginModule loginModule) : base(navigationService){
            _loginModule = loginModule;
            Title = "Login";
            OpenCompanyURICommand = new DelegateCommand(OpenCompanyURI);
        }

        public DelegateCommand OpenCompanyURICommand { get; set; }

        private void OpenCompanyURI()
        {
            Device.OpenUri(new Uri(_loginModule.GetUrlCompamy()));
        }
    }
}
