using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Movies.Services
{
    public interface IAzureLogin
    {
        Task LoginAsync(MobileServiceClient mobileServiceClient, MobileServiceAuthenticationProvider provider);
    }
}