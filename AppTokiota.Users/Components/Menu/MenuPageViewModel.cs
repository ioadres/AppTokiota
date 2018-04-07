using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Components.Login;
using AppTokiota.Users.Components.Master;
using AppTokiota.Users.Components.Timesheet;
using AppTokiota.Users.Components;
using AppTokiota.Users.Components.DashBoard;

namespace AppTokiota.Users.Components.Menu
{
    public class MenuPageViewModel : ViewModelBase
    {
        private readonly IMenuModule _menuModule;

        public ObservableCollection<MenuItem> MenuList
        {
            get;
            set;
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }


        public DelegateCommand<MenuItem> ItemTappedCommand => new DelegateCommand<MenuItem>(OnItemTapped);

        public DelegateCommand SignOutCommand => new DelegateCommand(SignOut);


        public MenuPageViewModel(IViewModelBaseModule baseModule, IMenuModule menuModule) : base(baseModule)
        {
            Title = "MenuPage";
            _menuModule = menuModule;

            MenuList = new ObservableCollection<MenuItem>();

            Task.Run(async() =>
            {
                var user = await BaseModule.CacheEntity.GetObjectAsync<User>(AppSettings.IdAppUserCache);
                if (user != null) Email = user.Email;
            });
           
            LoadMenu();
        }
       

        private void OnItemTapped(MenuItem menuItem)
        {
            NavigateCommand.Execute(menuItem.PageName);
        }

        private async void SignOut()
        {
            await BaseModule.AuthenticationService.Logout();
            NavigateCommand.Execute(PageRoutes.GetKey<LoginPage>());
        }

        private void LoadMenu()
        {
            MenuList.Add(new MenuItem()
            {
                Icon = "\uf0e4",
                Title = "Dashboard",
                PageName = MasterModule.GetMasterNavigationPage(PageRoutes.GetKey<DashBoardPage>())
            });
            MenuList.Add(new MenuItem()
            {
                Icon = "\uf073",
                Title = "Timesheet",
                PageName = MasterModule.GetMasterNavigationPage(PageRoutes.GetKey<TimesheetPage>())
            });
            MenuList.Add(new MenuItem()
            {
                Icon = "\uf085",
                Title = "Configuration"
            });
        }
    }
}