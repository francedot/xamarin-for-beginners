using Xamarin.Forms;

namespace MyReddit.Controls
{
    public class FontAwesomeIcon : Label
    {
        public const string Typeface = "FontAwesome";

        public FontAwesomeIcon()
        {
            FontFamily = Typeface;
        }

    }
}