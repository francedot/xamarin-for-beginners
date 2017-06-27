using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Movies.Services;
using Movies.UWP.Services;

[assembly: Xamarin.Forms.Dependency(typeof(AzureLogin))]
namespace Movies.UWP.Services
{
    public class AzureLogin : IAzureLogin
    {
        public Task LoginAsync(MobileServiceClient mobileServiceClient, MobileServiceAuthenticationProvider provider)
        {
            return mobileServiceClient.LoginAsync(provider);
        }
    }
}