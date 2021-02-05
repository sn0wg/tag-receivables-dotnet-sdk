using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TagSDK.Models;
using TagSDK.Models.Authentication;
using TagSDK.Models.Enums;
using TagSDK.Services.Interfaces;

namespace TagSDK.Authorization
{
    public class DefaultAuthorizator : IAuthorizator
    {
        protected readonly IAuthenticateService authenticateService;
        protected readonly SDKOptions options;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public DefaultAuthorizator(IAuthenticateService  authenticateService, SDKOptions options)
        {
            this.authenticateService = authenticateService;
            this.options = options;
        }

        private Dictionary<Profile, string> token = new Dictionary<Profile, string>();
        public async Task<string> GetToken(Profile profile, bool refresh = false)
        {
            try
            {
                await _semaphore.WaitAsync();

                if (refresh || !token.ContainsKey(profile))
                {                    

                    TokenResponse response = await authenticateService.GetToken(options.getCredential(profile));

                    this.token[profile] = response.AccessToken;
                }
            }
            finally
            {
                _semaphore.Release();
            }

            return token[profile];
        }
    }
}
