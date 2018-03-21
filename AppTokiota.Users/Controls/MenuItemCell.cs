using System;
using Xamarin.Forms;
namespace AppTokiota.Users.Controls
{
    public class MenuItemCell : ViewCell
    {
        public static readonly BindableProperty BackgroundColorSelectedProperty =
            BindableProperty.Create(nameof(BackgroundColorSelected), typeof(Color), typeof(MenuItemCell), Color.Default);

        public Color BackgroundColorSelected
        {
            get { return (Color)GetValue(BackgroundColorSelectedProperty); }
            set => SetValue(BackgroundColorSelectedProperty, value);
        }
    }
}
