using AppTokiota.Components.Core;
using AppTokiota.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Components.Menu
{
    public class MenuPageViewModel : ViewModelBase
    {
        public ObservableCollection<MenuItem> MenuList
        {
            get;
            set;
        }
        public MenuPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "MenuPage";
            MenuList = new ObservableCollection<MenuItem>();
            LoadMenu();
        }

        private void LoadMenu()
        {

            MenuList.Add(new MenuItem()
            {
                Icon = "&#xf2b9;",
                Title = "Dashboard"
            });
            MenuList.Add(new MenuItem()
            {
                Icon = "&#xf2bc;",
                Title = "Timesheet"
            });
            MenuList.Add(new MenuItem()
            {
                Icon = "&#xf042;",
                Title = "Configuration"
            });
        }
    }
}