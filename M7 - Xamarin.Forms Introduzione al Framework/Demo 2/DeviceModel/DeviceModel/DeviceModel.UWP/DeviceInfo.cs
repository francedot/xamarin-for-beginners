using Windows.Security.ExchangeActiveSyncProvisioning;
using DeviceModel.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceInfo))]
namespace DeviceModel.UWP
{
    public class DeviceInfo : IDeviceInfo
    {
        readonly EasClientDeviceInformation _deviceInfo;

        public DeviceInfo()
        {
            _deviceInfo = new EasClientDeviceInformation();
        }

        public string GetDeviceModel()
        {
            return _deviceInfo.FriendlyName;
        }
    }
}