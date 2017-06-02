using System;
using Effect.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("XamForBeginners")]
[assembly: ExportEffect(typeof(IOSFontAwesomeEffect), "FontAwesomeEffect")]
namespace Effect.iOS.Effects
{
    public class IOSFontAwesomeEffect : PlatformEffect
    {
        private UIFont _oldFont;

        protected override void OnAttached()
        {
            var formsLabel = Element as Label;
            if (formsLabel == null)
            {
                return;
            }

            var label = Control as UILabel;
            if (label == null)
            {
                return;
            }

            _oldFont = label.Font;

            label.Font = UIFont.FromName("FontAwesome", (nfloat) formsLabel.FontSize);
        }

        protected override void OnDetached()
        {
            if (_oldFont == null)
            {
                return;
            }

            var label = Control as UILabel;
            if (label != null)
            {
                label.Font = _oldFont;
            }
        }
    }
}