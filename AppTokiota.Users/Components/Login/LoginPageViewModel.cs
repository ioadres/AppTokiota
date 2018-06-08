using System;
using Prism.Commands;
using Xamarin.Forms;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Components;
using AppTokiota.Users.Components.Master;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Validations;
using AppTokiota.Users.Services;
using AppTokiota.Users.Components.DashBoard;
using Microsoft.AppCenter.Crashes;

namespace AppTokiota.Users.Components.Login
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly ILoginModule _loginModule;

        private ValidatableObject<string> _email;
        private ValidatableObject<string> _password;

        public ValidatableObject<string> Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public ValidatableObject<string> Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public LoginPageViewModel(IViewModelBaseModule baseModule, ILoginModule loginModule) : base(baseModule)
        {
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
        private void SignIn()
        {
			IsBusy = true;
			Device.BeginInvokeOnMainThread(async () =>
		    {
			   try
			   {
				   if (Validate())
					{
						if (this.IsInternetAndCloseModal())
					    {
							var responseRequest = await BaseModule.AuthenticationService.Login(_email.Value, _password.Value);
							IsBusy = false;
							if (responseRequest.Success)
						    {
							    NavigateCommand.Execute(MasterModule.GetMasterNavigationPage(PageRoutes.GetKey<DashBoardPage>()));
						    }
						    else
						    {							  
							   await BaseModule.DialogService.ShowAlertAsync(responseRequest.Message, "Login error", "Ok");
						    }
						}
					} else {
						IsBusy = false;
					}
			   }
			   catch (Exception e)
			   {
				   IsBusy = false;
				   BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
                   Crashes.TrackError(e);
			   }
		   });
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
