using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace MyReddit.Converters
{
    public class DummyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }
    }
}