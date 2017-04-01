using Xamarin.Forms;
using DeviceModel.Droid;

[assembly: Dependency(typeof(DeviceInfo))]
namespace DeviceModel.Droid
{
    public class DeviceInfo : IDeviceInfo
    {
        public string GetDeviceModel()
        {
            return Android.OS.Build.Model;
        }
    }
}