using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(AppTokiota.Droid.Renderers.RoundButtonRenderer))]
namespace AppTokiota.Droid.Renderers
{
    /// <summary>
    /// Overrides AppCompat problems with round buttons. Works like a charm!
    /// </summary>
    public class RoundButtonRenderer : ButtonRenderer
    {
        public RoundButtonRenderer(Context context) : base(context)
        {
           
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
        }
    }
}