using Prism.Ioc;
using AppTokiota.Users.Services;
using AppTokiota.Users.Components.Core.Module;

namespace AppTokiota.Users.Components.Login
{
    public class LoginModule: ILoginModule
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;

        public IAuthenticationService AuthenticationService => _authenticationService;
        public IDialogService DialogService => _dialogService;

        public LoginModule(IAuthenticationService authenticationService, IDialogService dialogService) {
            _authenticationService = authenticationService;
            _dialogService = dialogService;
        }
    }
}
