using System;
using System.Reflection;
using AppTokiota.iOS.Renderers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(StandardViewCellRenderer))]
namespace AppTokiota.iOS.Renderers
{
    public class StandardViewCellRenderer: ViewCellRenderer
    {

        public override UIKit.UITableViewCell GetCell(Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            switch (item.StyleId)
            {
                case "none":
                    cell.Accessory = UIKit.UITableViewCellAccessory.None;
                    break;
                case "checkmark":
                    cell.Accessory = UIKit.UITableViewCellAccessory.Checkmark;
                    break;
                case "detail-button":
                    cell.Accessory = UIKit.UITableViewCellAccessory.DetailButton;
                    break;
                case "detail-disclosure-button":
                    cell.Accessory = UIKit.UITableViewCellAccessory.DetailDisclosureButton;
                    cell.SelectionStyle  = UITableViewCellSelectionStyle.None;
                    break;
                case "disclosure":
                default:
                    cell.Accessory = UIKit.UITableViewCellAccessory.DisclosureIndicator;
                    cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    break;
            }

            CGRect rect = new CGRect(0, 0, 1, 1);
            CGSize size = rect.Size;
            UIGraphics.BeginImageContext(size);
            CGContext currentContext = UIGraphics.GetCurrentContext();
            currentContext.SetFillColor(Color.Green.ToCGColor());
            currentContext.FillRect(rect);
            var backgroundImage = UIGraphics.GetImageFromCurrentImageContext();
            currentContext.Dispose();
            CGContext currentNormalContext = UIGraphics.GetCurrentContext();
            currentNormalContext.SetFillColor(Color.FromHex("AC4E4C").ToCGColor());
            currentNormalContext.FillRect(rect);
            var normalBackgroundImage = UIGraphics.GetImageFromCurrentImageContext();
            currentNormalContext.Dispose();

            var globalContextViewCell = Type.GetType("Xamarin.Forms.Platform.iOS.ContextActionsCell, Xamarin.Forms.Platform.iOS, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null");

            // Now change the static field value! "NormalBackground" OR "DestructiveBackground"
            if (globalContextViewCell != null)
            {
                var normalButton = globalContextViewCell.GetField("NormalBackground", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                if (normalButton != null)
                {
                    normalButton.SetValue(null, backgroundImage);
                }

                var destructiveButton = globalContextViewCell.GetField("DestructiveBackground", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                if (destructiveButton != null)
                {
                    destructiveButton.SetValue(null, normalBackgroundImage);
                }
            }

            return cell;
        }

    }
}
