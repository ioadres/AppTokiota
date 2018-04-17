﻿
using Android.Support.V7.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AView = Android.Views.View;
using AppTokiota.Users.Components.BaseNavigation;
using AppTokiota.Droid.Renderers;
using Android.Content;

//[assembly: ExportRenderer(typeof(BaseNavigationPage), typeof(BaseNavigationPageRenderer))]
namespace AppTokiota.Droid.Renderers
{
    public class BaseNavigationPageRenderer : NavigationPageRenderer
    {
        public BaseNavigationPageRenderer(Context context) : base(context)
        {
        }

        IPageController PageController => Element as IPageController;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            int containerHeight = b - t;

            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            for (var i = 0; i < ChildCount; i++)
            {
                AView child = GetChildAt(i);

                if (child is Toolbar)
                {
                    continue;
                }

                child.Layout(0, 0, r, b);
            }
        }
    }
}