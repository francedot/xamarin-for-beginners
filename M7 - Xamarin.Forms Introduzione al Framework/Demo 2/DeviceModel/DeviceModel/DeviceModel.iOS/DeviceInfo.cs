using Xamarin.Forms;
using DeviceInfo = DeviceModel.iOS.DeviceInfo;

[assembly: Dependency(typeof(DeviceInfo))]
namespace DeviceModel.iOS
{
    public class DeviceInfo : IDeviceInfo
    {
        public string GetDeviceModel()
        {
            return UIKit.UIDevice.CurrentDevice.Model;
        }
    }
}