using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Movies.Droid.Services;
using Movies.Services;

[assembly:Xamarin.Forms.Dependency(typeof(AzureLogin))]
namespace Movies.Droid.Services
{
    public class AzureLogin : IAzureLogin
    {
        public Task LoginAsync(MobileServiceClient mobileServiceClient, MobileServiceAuthenticationProvider provider)
        {
            return mobileServiceClient.LoginAsync(Xamarin.Forms.Forms.Context, provider);
        }
    }
}