using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using AppTokiota.Users.Services;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Splash;
using Akavache;

namespace AppTokiota.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {

            InitializeComponent();
            BlobCache.ApplicationName = AppSettings.IdAppAkavache;
            await NavigationService.NavigateAsync(SplashModule.Tag);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //# Container Services
            ServicesLoader.Load(containerRegistry);

            //# Container Components
            ModuleLoader.Load(containerRegistry);
        }
    }
}