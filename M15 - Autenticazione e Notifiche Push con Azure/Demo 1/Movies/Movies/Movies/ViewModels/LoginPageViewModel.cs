using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Movies.Services;

namespace Movies.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public MobileAppAuthenticator Authenticator => MobileAppAuthenticator.Instance;

        public LoginPageViewModel()
        {
        }

        public async Task<bool> TryLoginAsync(string provider)
        {
            MobileServiceAuthenticationProvider authenticationProvider;
            if (!Enum.TryParse(provider, out authenticationProvider))
            {
                return false;
            }

            try
            {
                await Authenticator.LoginAsync(authenticationProvider);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}