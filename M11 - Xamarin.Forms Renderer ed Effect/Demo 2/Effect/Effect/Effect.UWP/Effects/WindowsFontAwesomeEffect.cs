using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Effect.UWP.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("XamForBeginners")]
[assembly: ExportEffect(typeof(WindowsFontAwesomeEffect), "FontAwesomeEffect")]
namespace Effect.UWP.Effects
{
    public class WindowsFontAwesomeEffect : PlatformEffect
    {
        private FontFamily _oldFontFamily;
        protected override void OnAttached()
        {
            if (Element is Label == false)
                return;

            var tb = Control as TextBlock;

            if (tb == null)
            {
                return;
            }

            _oldFontFamily = tb.FontFamily;
            tb.FontFamily = new FontFamily(@"/Assets/fontawesome.ttf#FontAwesome");
        }

        protected override void OnDetached()
        {
            if (_oldFontFamily == null)
            {
                return;
            }

            var tb = Control as TextBlock;
            if (tb != null)
            {
                tb.FontFamily = _oldFontFamily;
            }
        }
    }
}
