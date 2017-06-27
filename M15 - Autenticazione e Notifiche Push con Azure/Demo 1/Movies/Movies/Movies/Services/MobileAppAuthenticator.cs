using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.SecureStorage;
using Xamarin.Forms;

namespace Movies.Services
{
    public class MobileAppAuthenticator
    {
        private static readonly string MobileAppUrl = "https://moviesauth.azurewebsites.net";
        private readonly string _userIdKey = ":UserId";
        private readonly string _tokenKey = ":Token";
        private static MobileAppAuthenticator _instance;
        private MobileServiceClient _mobileServiceClient;
        private readonly IAzureLogin _authenticator = DependencyService.Get<IAzureLogin>();

        private MobileAppAuthenticator()
        {
        }

        public static MobileAppAuthenticator Instance => _instance ?? (_instance = new MobileAppAuthenticator());

        public MobileServiceClient ServiceClient => _mobileServiceClient;

        private void Initialize()
        {
            if (ServiceClient == null)
            {
                _mobileServiceClient = new MobileServiceClient(MobileAppUrl);
            }
        }

        public bool IsUserLogged()
        {
            Initialize();

            if (!CrossSecureStorage.Current.HasKey(_userIdKey) ||
                !CrossSecureStorage.Current.HasKey(_tokenKey))
            {
                return false;
            }

            var userId = CrossSecureStorage.Current.GetValue(_userIdKey);
            var token = CrossSecureStorage.Current.GetValue(_tokenKey);

            ServiceClient.CurrentUser = new MobileServiceUser(userId)
            {
                MobileServiceAuthenticationToken = token
            };
            return true;
        }

        public async Task LoginAsync(MobileServiceAuthenticationProvider authenticationProvider)
        {
            Initialize();
            await _authenticator.LoginAsync(_mobileServiceClient, authenticationProvider);

            var user = ServiceClient.CurrentUser;
            if (user != null)
            {
                CrossSecureStorage.Current.SetValue(_userIdKey, user.UserId);
                CrossSecureStorage.Current.SetValue(_tokenKey, user.MobileServiceAuthenticationToken);
            }
        }

        public async Task LogoutAsync()
        {
            await _mobileServiceClient.LogoutAsync();
            CrossSecureStorage.Current.DeleteKey(_userIdKey);
            CrossSecureStorage.Current.DeleteKey(_tokenKey);
        }
    }
}