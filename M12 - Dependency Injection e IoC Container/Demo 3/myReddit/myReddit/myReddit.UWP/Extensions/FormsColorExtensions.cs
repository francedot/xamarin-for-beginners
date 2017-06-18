using Windows.UI.Xaml.Markup;
using Color = Xamarin.Forms.Color;

namespace myReddit.UWP.Extensions
{

    public static class FormsColorExtensions
    {
        public static string ToArgbHexString(this Color color)
        {
            var colorAsHex = $"#{(int)(color.A * 255):X2}{(int)(color.R * 255):X2}{(int)(color.G * 255):X2}{(int)(color.B * 255):X2}";
            return colorAsHex;
        }

        public static Windows.UI.Color ToUWPColor(this Color color)
        {
            return (Windows.UI.Color)XamlBindingHelper.ConvertValue(typeof(Windows.UI.Color), color.ToArgbHexString());
        }
    }
}