﻿using RestSharp;
using System.Threading.Tasks;
using TagSDK.Extensions;
using TagSDK.Services.Interfaces;
using System;
using TagSDK.Models.Authentication;

namespace TagSDK.Services
{
    public class AuthenticateService : BaseService, IAuthenticateService
    {
        private const string Path = Constants.Constants.Authentication.BasePath;

        public AuthenticateService(IServiceProvider provider, SDKOptions options): 
            base(provider,options)
        {
        }

        public async Task<TokenResponse> GetToken(TokenRequest model)
        {
            var request = new RestRequest($"{options.BaseUrl}/{Path}", DataFormat.Json);
            request.Method = Method.POST;
            request.AddJsonBody(model);

            return await GetPipeline<TokenRequest, TokenResponse>().Execute(new Commands.RequestCommand<TokenRequest, TokenResponse>()
            {
                Authenticate = false,
                Model = model,
                RestRequest = request
            }).MapResponse();
        }
    }
}
