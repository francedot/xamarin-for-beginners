using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DeviceModel
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //InitializeComponent();

            var deviceInfo = DependencyService.Get<IDeviceInfo>();

            var label = new Label
            {
                Text = $"Device: {deviceInfo.GetDeviceModel()}",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Content = label;
        }
    }
}
