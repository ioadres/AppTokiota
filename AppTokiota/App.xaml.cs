using Xamarin.Forms.Xaml;
using AppTokiota.Views;
using AppTokiota.ViewModels;
using Prism.Unity;
using Prism;
using Prism.Ioc;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppTokiota
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected async override void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        }


    }
}
