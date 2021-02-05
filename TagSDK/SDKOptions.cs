using System.Collections.Generic;
using TagSDK.Models.Authentication;
using TagSDK.Models.Enums;

namespace TagSDK
{
    public class SDKOptions 
    {
        public string BaseUrl { get; set; }
        public Polly.AsyncPolicy DefaultPolicy { get; set; }

        private Dictionary<Profile, TokenRequest> Credentials = new Dictionary<Profile, TokenRequest>();

        public TokenRequest getCredential(Profile profile)
        {
            return this.Credentials[profile];
        }

        public void SetCredential(TokenRequest tokenInput, Profile profile)
        {
            this.Credentials[profile] = tokenInput;
        }
    }
}
