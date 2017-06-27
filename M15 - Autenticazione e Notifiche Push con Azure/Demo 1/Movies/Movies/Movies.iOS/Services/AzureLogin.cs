using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Movies.iOS.Services;
using Movies.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(AzureLogin))]
namespace Movies.iOS.Services
{
    public class AzureLogin : IAzureLogin
    {
        public Task LoginAsync(MobileServiceClient mobileServiceClient, MobileServiceAuthenticationProvider provider)
        {
            return mobileServiceClient.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, provider);
        }
    }
}