using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTokiota.Users.Controls
{
    public class HideToolbarItemControl : ToolbarItem
    {
            public HideToolbarItemControl() : base()
            {
                this.InitVisibility();
            }

            private async void InitVisibility()
            {
                await Task.Delay(100);
                OnIsVisibleChanged(this, false, IsVisible);
            }

            public ContentPage Parent { set; get; }

            public bool IsVisible
            {
                get { return (bool)GetValue(IsVisibleProperty); }
                set { SetValue(IsVisibleProperty, value); }
            }

            public static BindableProperty IsVisibleProperty =
                BindableProperty.Create<HideToolbarItemControl, bool>(o => o.IsVisible, false, propertyChanged: OnIsVisibleChanged);

            private static void OnIsVisibleChanged(BindableObject bindable, bool oldvalue, bool newvalue)
            {
                var item = bindable as HideToolbarItemControl;

                if (item.Parent == null)
                    return;

                var items = item.Parent.ToolbarItems;

                if (newvalue && !items.Contains(item))
                {
                    items.Add(item);
                }
                else if (!newvalue && items.Contains(item))
                {
                    items.Remove(item);
                }
            }
    }
}
