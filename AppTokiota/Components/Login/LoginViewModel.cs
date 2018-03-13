using System;
using AppTokiota.Attributes;
using AppTokiota.Components.Core;
using AppTokiota.Components.Core.Validations;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using AppTokiota.Components.Core.Module;

namespace AppTokiota.Components.Login
{
    public class LoginViewModel : ViewModelBase
    {

        private ILoginModule _loginModule { get; set; }

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


        public LoginViewModel(INavigationService navigationService, ILoginModule loginModule) : base(navigationService){
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
            Device.OpenUri(new Uri(_loginModule.GetUrlCompamy()));
        }

        public DelegateCommand SignInCommand => new DelegateCommand(SignIn);

        private async void SignIn()
        {
            bool isValid = Validate();

            if (isValid)
            {
               
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
