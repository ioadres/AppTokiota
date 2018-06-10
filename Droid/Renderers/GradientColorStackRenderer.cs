using System;
using Android.Content;
using AppTokiota.Users.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(GradientColorStack), typeof(AppTokiota.Droid.Renderers.GradientColorStackRenderer))]
namespace AppTokiota.Droid.Renderers
{
    public class GradientColorStackRenderer : VisualElementRenderer<Frame>
    {
        private Color StartColor { get; set; }
        private Color EndColor { get; set; }

        public GradientColorStackRenderer(Context context) : base(context)
        {
        }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            #region for Horizontal Gradient
            var gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0,
            #endregion

                   this.StartColor.ToAndroid(),
                   this.EndColor.ToAndroid(),
                   Android.Graphics.Shader.TileMode.Mirror);

            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                var stack = e.NewElement as GradientColorStack;
                this.StartColor = stack.StartColor;
                this.EndColor = stack.EndColor;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }
    }
}
