using System;
using AppTokiota.iOS.Renderers;
using AppTokiota.Users.Controls;
using CoreAnimation;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(GradientColorStack), typeof(GradientColorStackRenderer))]
namespace AppTokiota.iOS.Renderers
{
    public class GradientColorStackRenderer: VisualElementRenderer<Frame>
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            var stack = this.Element as GradientColorStack;

            CGColor startColor = stack.StartColor.ToCGColor();
            CGColor endColor = stack.EndColor.ToCGColor();
            #region for Vertical Gradient
            var gradientLayer = new CAGradientLayer();
            #endregion
            gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[] { startColor, endColor};
            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}
