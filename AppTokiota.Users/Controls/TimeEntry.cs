using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTokiota.Users.Controls
{
    public class TimeEntry : ContentView
    {
        #region DateCommand

        public static readonly BindableProperty SelectedTimeCommandProperty =
            BindableProperty.Create(nameof(SelectedTimeCommand), typeof(ICommand), typeof(Calendar), null);

        /// <summary>
        /// Gets or sets the Selected Time command.
        /// </summary>
        /// <value>The date command.</value>
        public ICommand SelectedTimeCommand
        {
            get { return (ICommand)GetValue(SelectedTimeCommandProperty); }
            set { SetValue(SelectedTimeCommandProperty, value); }
        }

        #endregion

        #region Content Visible

        public static readonly BindableProperty ContentViewVisibleProperty =
            BindableProperty.Create(nameof(ContentViewVisible), typeof(bool), typeof(TimeEntry), false,
                                    propertyChanged: (bindable, oldValue, newValue) => (bindable as TimeEntry).ChangeContentViewVisibleProperty((bool)newValue, (bool)oldValue));

        protected void ChangeContentViewVisibleProperty(bool newValue, bool oldValue)
        {
            if (newValue == oldValue) return;
            MainView.IsVisible = newValue;
        }

        /// <summary>
        /// Gets or sets the visibility  of the Content views.
        /// </summary>
        /// <value>The width of the selected border.</value>
        public bool ContentViewVisible
        {
            get { return (bool)GetValue(ContentViewVisibleProperty); }
            set { SetValue(ContentViewVisibleProperty, value); }
        }

        #endregion

        private ButtonTimeTask SelectedHour;
        private ButtonTimeTask SelectedMinute;
        StackLayout ContentView, MainView;
        List<ButtonTimeTask> ButtonsHour;
        List<ButtonTimeTask> ButtonsMinutes;
        Grid MainHours;
        Grid MainMinutes;

        public TimeEntry()
        {

            CreatedHours();
            CreatedMinutes();

            MainView = new StackLayout
            {
                Padding = 0,
                Children = { MainHours },
                IsVisible = false
            };

            ContentView = new StackLayout
            {
                Padding = 0,
                Children = { MainView }
            };

            this.Content = ContentView;
        }


        protected void CreatedHours()
        {

            var GridSpace = 1;
            var columDef = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) };
            var rowDef = new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) };

            MainHours = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = GridSpace,
                ColumnSpacing = GridSpace,
                Padding = 1
            };

            MainHours.ColumnDefinitions = new ColumnDefinitionCollection { columDef, columDef, columDef, columDef, columDef, columDef, columDef, columDef };
            MainHours.RowDefinitions = new RowDefinitionCollection { rowDef, rowDef, rowDef };

            ButtonsHour = new List<ButtonTimeTask>();

            for (var fila = 0; fila < 3; fila++)
            {
                for (var i = 1; i <= 8; i++)
                {
                    ButtonsHour.Add(new ButtonTimeTask()
                    {
                        BorderRadius = 0,
                        BorderWidth = 1,
                        BorderColor = Color.Gray,
                        FontSize = 10,
                        BackgroundColor = Color.Transparent,
                        TextColor = Color.DarkSalmon,
                        Text = $"{i + (fila * 8)} h",
                        WidthRequest = 50,
                        HeightRequest = 35,
                        Value = i.ToString(),
                    });
                    var b = ButtonsHour.Last();
                    b.Clicked += HourClickedEvent;

                    MainHours.Children.Add(b, i - 1, fila);
                }
            }
        }

        protected void CreatedMinutes()
        {
            var GridSpace = 1;
            var columDef = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) };
            var rowDef = new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) };

            MainMinutes = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = GridSpace,
                ColumnSpacing = GridSpace,
                Padding = 1,
            };

            MainMinutes.ColumnDefinitions = new ColumnDefinitionCollection { columDef, columDef, columDef, columDef };
            MainMinutes.RowDefinitions = new RowDefinitionCollection { rowDef };

            ButtonsMinutes = new List<ButtonTimeTask>();

            var count = 0;
            for (var i = 0; i <= 45; i = i + 15)
            {
                ButtonsMinutes.Add(new ButtonTimeTask()
                {
                    BorderRadius = 0,
                    BorderWidth = 1,
                    BorderColor = Color.Gray,
                    FontSize = 10,
                    BackgroundColor = Color.Transparent,
                    TextColor = Color.DarkSalmon,
                    Text = $"{i} m",
                    WidthRequest = 50,
                    HeightRequest = 35,
                    Value = i.ToString(),
                });
                var b = ButtonsMinutes.Last();
                b.Clicked += MinuteClickedEvent;

                MainMinutes.Children.Add(b, count, 0);
                count++;
            }
        }


        protected void MinuteClickedEvent(object s, EventArgs a)
        {
            var selected = (s as ButtonTimeTask);
            if (SelectedMinute != null)
            {
                SelectedMinute.BackgroundColor = Color.Transparent;
            }
            selected.BackgroundColor = Color.LightGreen;
            SelectedMinute = selected;

            Device.BeginInvokeOnMainThread(() =>
            {
                MainView.Children.Clear();
                MainView.Children.Add(MainHours);
                ContentViewVisible = false;
                MainView.ForceLayout();
                SelectedTimeCommand?.Execute($"{SelectedHour}:{SelectedMinute}");
            });
        }

        protected void HourClickedEvent(object s, EventArgs a)
        {
            var selectedHour = (s as ButtonTimeTask);
            if (SelectedHour != null)
            {
                SelectedHour.BackgroundColor = Color.Transparent;
            }
            selectedHour.BackgroundColor = Color.LightGreen;
            SelectedHour = selectedHour;

            Device.BeginInvokeOnMainThread(() =>
            {
                MainView.Children.Clear();
                MainView.Children.Add(MainMinutes);
            });

        }
    }
}
