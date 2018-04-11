using System;
using AppTokiota.iOS.Renderers;
using AppTokiota.Users.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(TimePickerDialogIntervals), typeof(CustomDateTimePickerRenderer))]
namespace AppTokiota.iOS.Renderers
{
    public class CustomDateTimePickerRenderer : DatePickerRenderer
    {
        public TimePickerDialogIntervals dtPicker;
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            dtPicker = e.NewElement as TimePickerDialogIntervals;

            if (Control != null)
            {
                var input = Control.InputView as UIDatePicker;

                input.Mode = UIDatePickerMode.Time;
                input.MinuteInterval = 15;
            }
        }
    }
}
