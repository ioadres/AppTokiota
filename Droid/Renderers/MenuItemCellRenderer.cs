using System;

[assembly: ExportRenderer(typeof(MenuItemCell), typeof(MenuItemCellRenderer))]
namespace AppTokiota.Droid.Renderers
{
    public class MenuItemCellRenderer : ViewCellRenderer
    {
        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context)
        {
            var cell = base.GetCellCore(item, convertView, parent, context);
            var view = item as MenuItemCell;

            if (view != null)
            {
                // Disable native cell selection color style
                view.SetSelector(cell.);
                view.CacheColorHint = Android.Graphics.Color.Transparent;
            }

            return cell;
        }
    }
}
