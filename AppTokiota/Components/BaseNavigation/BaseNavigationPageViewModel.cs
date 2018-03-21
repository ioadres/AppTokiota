using AppTokiota.Components.Core;
using AppTokiota.Components.Core.Module;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Components.BaseNavigation
{
    public class BaseNavigationPageViewModel : ViewModelBase
    {
        public BaseNavigationPageViewModel(IViewModelBaseModule baseModule) : base(baseModule)
        {
        }
    }
}
