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
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.ComponentModel;
using AppTokiota.Users.Controls;
using AppTokiota.Droid.Renderers;

[assembly: ExportRenderer(typeof(ExtendedEditor), typeof(ExtendedEditorRenderer))]
namespace AppTokiota.Droid.Renderers
{
    public class ExtendedEditorRenderer : Xamarin.Forms.Platform.Android.EditorRenderer
    {
        public ExtendedEditorRenderer(Context context)
            : base(context) {
        }
        
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var element = e.NewElement as ExtendedEditor;
                this.Control.Hint = element.Placeholder;
                this.Control.SetHintTextColor(element.PlaceholderColor.ToAndroid());
            }
        }

       

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ExtendedEditor.PlaceholderProperty.PropertyName)
            {
                var element = this.Element as ExtendedEditor;
                this.Control.Hint = element.Placeholder;
                this.Control.SetHintTextColor(element.PlaceholderColor.ToAndroid());
            }
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}