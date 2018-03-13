using Xamarin.Forms.Xaml;
using Prism.Unity;
using Prism;
using Prism.Ioc;
using AppTokiota.Components.Core;
using AppTokiota.Services;
using AppTokiota.Components.Core.Module;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppTokiota
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected async override void OnInitialized()
        {
            InitializeComponent();
            var loginModule = Container.Resolve<ILoginModule>();
            await NavigationService.NavigateAsync(loginModule.GetTag());
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ServicesLoader.Load(containerRegistry);
            ModuleLoader.Load(containerRegistry);

            var loginModule = Container.Resolve<ILoginModule>();
            loginModule.Register(containerRegistry);   
        }


    }
}
