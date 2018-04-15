using System;
using AppTokiota.iOS.Renderers;
using AppTokiota.Users.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page), typeof(CustomPageRenderer))]
namespace AppTokiota.iOS.Renderers
{
    public class CustomPageRenderer: PageRenderer
    {
        IElementController ElementController => Element as IElementController;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // Force nav bar text color
            var color = NavigationBarAttachedProperty.GetTextColor(Element);
            NavigationBarAttachedProperty.SetTextColor(Element, color);

            var background = NavigationBarAttachedProperty.GetBackgroundColor(Element);
            NavigationBarAttachedProperty.SetBackgroundColor(Element, background);
        }
    }
}