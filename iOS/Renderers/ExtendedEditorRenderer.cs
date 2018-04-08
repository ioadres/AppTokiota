
using System;
using System.ComponentModel;
using AppTokiota.iOS.Renderers;
using AppTokiota.Users.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEditor), typeof(ExtendedEditorRenderer))]
namespace AppTokiota.iOS.Renderers
{
    public class ExtendedEditorRenderer : EditorRenderer
    {
        public ExtendedEditor ExtendedEntryElement => Element as ExtendedEditor;

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control.Text == "")
                {
                    Control.Text = ExtendedEntryElement.Placeholder;
                }
                Control.ShouldBeginEditing += (UITextView textView) =>
                {
                    if (textView.Text == ExtendedEntryElement.Placeholder)
                    {
                        textView.Text = "";
                        textView.TextColor = ExtendedEntryElement.TextColor.ToUIColor(); // Text Color
                    }

                    return true;
                };

                Control.ShouldEndEditing += (UITextView textView) =>
                {
                    if (textView.Text == "")
                    {
                        textView.Text = ExtendedEntryElement.Placeholder;
                        textView.TextColor = ExtendedEntryElement.PlaceholderColor.ToUIColor(); // Text Color
                    }

                    return true;
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);           
        }


    }
}
