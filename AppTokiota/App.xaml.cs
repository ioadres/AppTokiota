using Xamarin.Forms.Xaml;
using Prism.Unity;
using Prism;
using Prism.Ioc;
using AppTokiota.Components.Login;
using AppTokiota.Components.Core.Interfaces;
using AppTokiota.Components.Core;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppTokiota
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected async override void OnInitialized()
        {
            InitializeComponent();
            var loginModule = Container.Resolve<BaseLoginModule>();
            await NavigationService.NavigateAsync(loginModule.Tag);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ModuleLoader.Load(containerRegistry);

            var loginModule = Container.Resolve<BaseLoginModule>();
            loginModule.Register(containerRegistry);   
        }


    }
}
