using AppTokiota.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppTokiota.Users.Controls
{
    public class LabelReviewControl : Label
    {
        private const string holiday = "\uf072";
        private const string weekend = "\uf118";
        private const string edit  = "f303"; //"\f044"; 
        private const int MAXSTRING = 100;

        public static readonly BindableProperty DayProperty =
           BindableProperty.Create("Day", typeof(Day), typeof(LabelReviewControl), null,
                propertyChanged: (bindable, oldValue, newValue) => (bindable as LabelReviewControl).ChangeDay(newValue));
        private void ChangeDay(object newValue)
        {
            var day = (Day)newValue;
            if(string.IsNullOrEmpty(this.Text))
            {
                if (day.Holiday.IsHolyday) this.Text = holiday + "  Holydays!!";
                if (day.IsWeekend) this.Text = weekend + "  Weekend!!";
            }

        }

        public Day Day
        {
            get { return (Day)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }

        public static readonly BindableProperty ProjectDescriptionProperty =
           BindableProperty.Create("ProjectDescription", typeof(int), typeof(LabelReviewControl), 0,
               propertyChanged: (bindable, oldValue, newValue) => (bindable as LabelReviewControl).ChangeProjectDescription(newValue));

        private void ChangeProjectDescription(object newValue)
        {
            var value = (int)newValue;
            if (value == 0)
            { 
                this.Text = edit + "  Not imputed!!";
            }

        }

        public int ProjectDescription
        {
            get { return (int)GetValue(ProjectDescriptionProperty); }
            set { SetValue(ProjectDescriptionProperty, value); }
        }

    }
}
