using AppTokiota.Users.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AppTokiota.Users.Controls
{
    public class BindablePickerControl : Picker
    {
        public BindablePickerControl()
        {
            base.SelectedIndexChanged += OnSelectedIndexChanged;
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object), typeof(BindablePickerControl), null, BindingMode.OneWay, null, new BindableProperty.BindingPropertyChangedDelegate(BindablePickerControl.OnSelectedItemChanged), null, null, null);
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(BindablePickerControl), null, BindingMode.OneWay, null, new BindableProperty.BindingPropertyChangedDelegate(BindablePickerControl.OnItemsSourceChanged), null, null, null);
        public static readonly BindableProperty DisplayPropertyProperty = BindableProperty.Create("DisplayProperty", typeof(string), typeof(BindablePickerControl), null, BindingMode.OneWay, null, new BindableProperty.BindingPropertyChangedDelegate(BindablePickerControl.OnDisplayPropertyChanged), null, null, null);

        public IList ItemsSource
        {
            get { return (IList)base.GetValue(BindablePickerControl.ItemsSourceProperty); }
            set { base.SetValue(BindablePickerControl.ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return base.GetValue(BindablePickerControl.SelectedItemProperty); }
            set
            {
                base.SetValue(BindablePickerControl.SelectedItemProperty, value);
                if (ItemsSource.Contains(SelectedItem))
                {
                    SelectedIndex = ItemsSource.IndexOf(SelectedItem);
                }
                else
                {
                    SelectedIndex = -1;
                }
            }
        }

        public string DisplayProperty
        {
            get { return (string)base.GetValue(BindablePickerControl.DisplayPropertyProperty); }
            set { base.SetValue(BindablePickerControl.DisplayPropertyProperty, value); }
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedItem = ItemsSource[SelectedIndex];
        }


        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BindablePickerControl picker = (BindablePickerControl)bindable;
            picker.SelectedItem = newValue;
            if (picker.ItemsSource != null && picker.SelectedItem != null)
            {
                int count = 0;
                foreach (object obj in picker.ItemsSource)
                {
                    if (obj == picker.SelectedItem)
                    {
                        picker.SelectedIndex = count;
                        break;
                    }
                    count++;
                }
            }
        }

        private static void OnDisplayPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BindablePickerControl picker = (BindablePickerControl)bindable;
            picker.DisplayProperty = (string)newValue;
            loadItemsAndSetSelected(bindable);

        }
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BindablePickerControl picker = (BindablePickerControl)bindable;
            picker.ItemsSource = (IList)newValue;
            loadItemsAndSetSelected(bindable);
        }

        static void loadItemsAndSetSelected(BindableObject bindable)
        {
            BindablePickerControl picker = (BindablePickerControl)bindable;
            if (picker.ItemsSource as IEnumerable != null)
            {
                int count = 0;
                foreach (object obj in (IEnumerable)picker.ItemsSource)
                {
                    string value = string.Empty;
                    if (picker.DisplayProperty != null)
                    {
                        var prop = obj.GetType().GetRuntimeProperties().FirstOrDefault(p => string.Equals(p.Name, picker.DisplayProperty, StringComparison.OrdinalIgnoreCase));
                        if (prop != null)
                        {
                            value = prop.GetValue(obj).ToString();
                        }
                    }
                    else
                    {
                        value = obj.ToString();
                    }
                    picker.Items.Add(value);
                    if (picker.SelectedItem != null)
                    {
                        if (picker.SelectedItem == obj)
                        {
                            picker.SelectedIndex = count;
                        }
                    }
                    count++;
                }
            }
        }
    }
}
