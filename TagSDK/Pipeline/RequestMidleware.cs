using System;
using Polly;
using System.Threading.Tasks;
using RestSharp;
using TagSDK.Commands;
using TagSDK.Authorization;
using TagSDK.Models.Enums;

namespace TagSDK.Pipeline
{

    public class RequestMidleware<T,T2> : Filter<RequestCommand<T, T2>, ResponseCommand<T2>>
    {
        
        protected IRestClient restClient;
        protected IRestRequest request;
        protected IAuthorizator authorizator;

        public RequestMidleware(IRestClient restClient, IAuthorizator authorizator)
        {
            this.restClient = restClient;
            this.authorizator = authorizator;
        }
            
        protected override async Task<ResponseCommand<T2>> Execute(RequestCommand<T,T2> context, Func<RequestCommand<T, T2>, Task<ResponseCommand<T2>>> next)            
        {

            this.request = context.RestRequest;
            Profile profile = context.Profile;

            Polly.AsyncPolicy<IRestResponse<T2>> _policy = Policy.NoOpAsync<IRestResponse<T2>>();

            if (context.Authenticate)
            {
                _policy = context.CustomPolicy ?? Policy.NoOpAsync<IRestResponse<T2>>();

                Polly.AsyncPolicy<IRestResponse<T2>> mainPolicy = Policy
                    .HandleResult<IRestResponse<T2>>(resp => resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    .RetryAsync(
                        retryCount: 1,
                        onRetryAsync: async (_result, _retryNumber, _ctx) => await GetAuthorizationTokenAsync(profile, true)
                        );

                _policy = mainPolicy.WrapAsync(_policy);


                await GetAuthorizationTokenAsync(profile);
            }

            var result = await _policy.ExecuteAsync(async () => await restClient.ExecuteAsync<T2>(context.RestRequest));

            ResponseCommand<T2> response = new ResponseCommand<T2>();
            response.Response = result;

            return response;
        }

        protected async Task GetAuthorizationTokenAsync(Profile profile, bool refresh = false)
        {
            string token = await this.authorizator.GetToken(profile, refresh);
            
            this.request.AddHeader("Authorization", $"Bearer {token}");
        }
    }
}
