﻿using AppTokiota.Components.Core;
using AppTokiota.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Components.Core.Module;
using AppTokiota.Components.Login;

namespace AppTokiota.Components.Menu
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


        public MenuPageViewModel(INavigationService navigationService, IMenuModule menuModule) : base(navigationService)
        {
            _menuModule = menuModule;
            Title = "MenuPage";
            MenuList = new ObservableCollection<MenuItem>();
            Email = AppSettings.User.Email;
            LoadMenu();
        }
       

        private void OnItemTapped(MenuItem menuItem)
        {
            NavigateCommand.Execute(menuItem.PageName);
        }

        private async void SignOut()
        {
            await _menuModule.AuthenticationService.Logout();
            NavigateCommand.Execute(LoginModule.Tag);
        }

        private void LoadMenu()
        {

            MenuList.Add(new MenuItem()
            {
                Icon = "\uf0e4",
                Title = "Dashboard"
            });
            MenuList.Add(new MenuItem()
            {
                Icon = "\uf073",
                Title = "Timesheet",
                PageName = LoginModule.Tag
            });
            MenuList.Add(new MenuItem()
            {
                Icon = "\uf085",
                Title = "Configuration"
            });
        }
    }
}