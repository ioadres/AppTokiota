using AppTokiota.Components.Core;
using AppTokiota.Components.Core.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;

namespace AppTokiota.Components.Dashboard
{
    public class DashBoardViewModel : ViewModelBase
    {
        private readonly IDashBoardModule _dashBoardModule;

        public DashBoardViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
