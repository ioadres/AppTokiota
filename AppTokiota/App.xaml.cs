using Xamarin.Forms.Xaml;
using Prism.Unity;
using Prism;
using Prism.Ioc;
using AppTokiota.Components.Core;
using AppTokiota.Services;
using AppTokiota.Components.Core.Module;
using AppTokiota.Components;
using AppTokiota.Components.Login;
using AppTokiota.Components.Splash;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppTokiota
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected async override void OnInitialized()
        {
            InitializeComponent();
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
