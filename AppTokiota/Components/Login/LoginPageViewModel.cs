using System;
using AppTokiota.Components.Core;
using AppTokiota.Components.Core.Validations;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using AppTokiota.Components.Core.Module;
using AppTokiota.Components.Dashboard;
using AppTokiota.Components.BaseNavigation;
using AppTokiota.Components.Master;
using AppTokiota.Services.Authentication;

namespace AppTokiota.Components.Login
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly ILoginModule _loginModule;

        private ValidatableObject<string> _email;
        private ValidatableObject<string> _password;

        public ValidatableObject<string> Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value);}
        }

        public ValidatableObject<string> Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public LoginPageViewModel(IViewModelBaseModule baseModule, ILoginModule loginModule) : base(baseModule){
            _loginModule = loginModule;

            Title = "Login";

            OpenCompanyURICommand = new DelegateCommand(OpenCompanyURI);

            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            AddValidations();
        }

        public DelegateCommand OpenCompanyURICommand { get; set; }
        private void OpenCompanyURI()
        {
            Device.OpenUri(new Uri(AppSettings.UrlCompany));
        }

        public DelegateCommand SignInCommand => new DelegateCommand(SignIn);
        private async void SignIn()
        {
            try
            {
                IsBusy = true;
                if (Validate())
                {
                    var responseRequest = await BaseModule.AuthenticationService.Login(_email.Value, _password.Value);
                    if (responseRequest.Success)
                    {
                       NavigateCommand.Execute(MasterModule.GetMasterNavigationPage(DashBoardModule.Tag));
                    }
                    else
                    {
                        await BaseModule.DialogService.ShowAlertAsync(responseRequest.Message, "Login error", "Ok");
                    }
                }
            }
            catch (ServiceAuthenticationException)
            {
                await BaseModule.DialogService.ShowAlertAsync("Please, try again", "Login error", "Ok");
            }
            catch (Exception)
            {
                await BaseModule.DialogService.ShowAlertAsync("An error occurred, try again", "Error", "Ok");
            }
            finally
            {
                IsBusy = false;
            }            
        }

        private void AddValidations()
        {
            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Username should not be empty" });
            _email.Validations.Add(new EmailRule());
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password should not be empty" });
        }

        private bool Validate()
        {
            bool isValidUser = _email.Validate();
            bool isValidPassword = _password.Validate();

            return isValidUser && isValidPassword;
        }
    }
}
