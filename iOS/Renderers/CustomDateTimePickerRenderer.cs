using System;
using AppTokiota.iOS.Renderers;
using AppTokiota.Users.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(CustomDateTimePicker), typeof(CustomDateTimePickerRenderer))]
namespace AppTokiota.iOS.Renderers
{
    public class CustomDateTimePickerRenderer : DatePickerRenderer
    {
        public CustomDateTimePicker dtPicker;
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            dtPicker = e.NewElement as CustomDateTimePicker;

            if (Control != null)
            {
                var input = Control.InputView as UIDatePicker;

                input.Mode = UIDatePickerMode.Time;
                input.MinuteInterval = 15;
            }
        }
    }
}
