using System;
using AppTokiota.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreMedia;
using AppTokiota.iOS.Renderers;

[assembly: ExportRenderer(typeof(MenuItemCell), typeof(MenuItemCellRenderer))]
namespace AppTokiota.iOS.Renderers
{
    public class MenuItemCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var view = item as MenuItemCell;

            if (view != null)
            {
                //cell.SelectionStyle = UITableViewCellSelectionStyle.None;

                cell.SelectedBackgroundView = new UIView
                {
                    BackgroundColor = view.BackgroundColorSelected.ToUIColor(),
                };
            }

            return cell;
        }

    }
}
