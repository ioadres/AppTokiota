using System;
using Prism.Navigation;

namespace AppTokiota.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Login";
        }
    }
}
