using LoadSheddingApp.Services.Interfaces;
using Microsoft.Identity.Client;
using System.Diagnostics;
using Microsoft.Datasync.Client;
using LoadSheddingApp.Configuration;

namespace LoadSheddingApp.Services
{
    public class IdentityClientService : IIdentityService
    {
        private ISettings _settings;
        public IPublicClientApplication IdentityClient { get; set; }

        public IdentityClientService(ISettings settings)
        {
            _settings = settings;
        }

        public async Task<AuthenticationToken> GetAuthenticationToken()
        {
            if (IdentityClient == null)
            {
#if ANDROID
        IdentityClient = PublicClientApplicationBuilder
            .Create(_settings.ApplicationId)
            .WithAuthority(AzureCloudInstance.AzurePublic, "common")
            .WithRedirectUri($"msal{_settings.ApplicationId}://auth")
            .WithParentActivityOrWindow(() => Platform.CurrentActivity)
            .Build();
#elif IOS
        IdentityClient = PublicClientApplicationBuilder
            .Create(_settings.ApplicationId)
            .WithAuthority(AzureCloudInstance.AzurePublic, "common")
            .WithIosKeychainSecurityGroup("com.microsoft.adalcache")
            .WithRedirectUri($"msal{_settings.ApplicationId}://auth")
            .Build();
#else
                IdentityClient = PublicClientApplicationBuilder
                    .Create(_settings.ApplicationId)
                    .WithAuthority(AzureCloudInstance.AzurePublic, "common")
                    .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
                    .Build();
#endif
            }

            var accounts = await IdentityClient.GetAccountsAsync();
            AuthenticationResult result = null;
            bool tryInteractiveLogin = false;

            try
            {
                result = await IdentityClient
                    .AcquireTokenSilent(_settings.Scopes, accounts.FirstOrDefault())
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                tryInteractiveLogin = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"MSAL Silent Error: {ex.Message}");
            }

            if (tryInteractiveLogin)
            {
                try
                {
                    result = await IdentityClient
                        .AcquireTokenInteractive(_settings.Scopes)
                        .ExecuteAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"MSAL Interactive Error: {ex.Message}");
                }
            }

            return new AuthenticationToken
            {
                DisplayName = result?.Account?.Username ?? "",
                ExpiresOn = result?.ExpiresOn ?? DateTimeOffset.MinValue,
                Token = result?.AccessToken ?? "",
                UserId = result?.Account?.Username ?? ""
            };
        }
    }
}
