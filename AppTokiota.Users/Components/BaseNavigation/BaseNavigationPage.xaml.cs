using AppTokiota.Users.Utils;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTokiota.Users.Components.BaseNavigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseNavigationPage : NavigationPage, INavigationPageOptions
    {
        public bool ClearNavigationStackOnNavigation
        {
            get { return true; }
        }

        public BaseNavigationPage() : base()
        {
            InitializeComponent();
        }

        public BaseNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }


        internal void ApplyNavigationTextColor(Page targetPage)
        {
            var color = NavigationBarAttachedProperty.GetTextColor(targetPage);
            BarTextColor = color == Color.Default
                ? Color.White
                : color;
        }

        internal void ApplyNavigationBackgroundColor(Page targetPage)
        {
			
            var color = NavigationBarAttachedProperty.GetBackgroundColor(targetPage);
            BarBackgroundColor = color == Color.Default
                                         ? (Color)App.Current.Resources["RedColor"]
                : color;
        }
    }
}