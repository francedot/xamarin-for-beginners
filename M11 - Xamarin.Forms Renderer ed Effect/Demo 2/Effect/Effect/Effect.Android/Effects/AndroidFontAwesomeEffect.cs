using Android.Graphics;
using Android.Widget;
using Effect.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("XamForBeginners")]
[assembly: ExportEffect(typeof(AndroidFontAwesomeEffect), "FontAwesomeEffect")]
namespace Effect.Droid.Effects
{
    public class AndroidFontAwesomeEffect : PlatformEffect
    {
        private Typeface _oldFont;

        protected override void OnAttached()
        {
            if (Element is Label == false)
                return;

            var label = Control as TextView;

            if (label == null)
            {
                return;
            }
            _oldFont = label.Typeface;

            var font = Typeface.CreateFromAsset(Forms.Context.Assets, "fontawesome.ttf"); 
            label.Typeface = font;
        }

        protected override void OnDetached()
        {
            if (_oldFont == null)
            {
                return;
            }
            var label = Control as TextView;

            if (label != null)
            {
                label.Typeface = _oldFont;
            }
        }
    }
}