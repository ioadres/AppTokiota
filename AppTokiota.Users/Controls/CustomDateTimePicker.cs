using System;
using Xamarin.Forms;

namespace AppTokiota.Users.Controls
{
    public class CustomDateTimePicker : DatePicker
    {
        private DateTime cdt;
        public event EventHandler<DateTimePickerArgs> DTChanged;
        public DateTime CDT
        {
            get { return cdt; }
            set
            {
                cdt = this.Date = value;
                if (DTChanged != null)
                    DTChanged.Invoke(this, new DateTimePickerArgs(cdt));
            }
        }
    }

    public class DateTimePickerArgs : EventArgs
    {
        private DateTime dt;
        public DateTimePickerArgs(DateTime NewDT)
        {
            dt = NewDT;
        }
        public DateTime DT { get { return dt; } }
    }
}
