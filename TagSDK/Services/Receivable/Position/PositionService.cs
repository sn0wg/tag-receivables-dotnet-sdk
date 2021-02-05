using RestSharp;
using System;
using System.Threading.Tasks;
using TagSDK.Extensions;
using TagSDK.Models.Enums;
using TagSDK.Models.Receivable.Position;
using TagSDK.Utils;

namespace TagSDK.Services.Receivable.Position
{
    public class PositionService : BaseService, IPositionService
    {


        private const string PathBaseKey = Constants.Constants.Position.BasePathKey;
        private const string PathBaseAssetHolder = Constants.Constants.Position.BasePathAssetHolder;
        private const string PathBaseOriginalAssetHolder = Constants.Constants.Position.BasePathOriginalAssetHolder;
        private const string PathBaseProcessReference = Constants.Constants.Position.BasePathProcessReference;
        private const string PathBaseReference = Constants.Constants.Position.BasePathReference;
        private Profile baseProfile = Profile.ACQUIRER;

        public PositionService(IServiceProvider serviceProvider, SDKOptions options) :
           base(serviceProvider, options)
        {
        }

        public async Task<PositionExpectationQueryResponse> GetPositionsWithAssetHolder(string assetHolder, PositionQueryFilter paramsObj)
        {
            var pathRequest = $"{options.BaseUrl}/{PathBaseAssetHolder}/{assetHolder}";


            var queryParams = new CustomQueryParams().ReturnQueryParams(paramsObj);
            if (!string.IsNullOrEmpty(queryParams))
                pathRequest = $"{pathRequest}{queryParams}";

            var request = new RestRequest(pathRequest, DataFormat.Json);
            request.Method = Method.GET;

            return await GetPipeline<PositionExpectationQueryResponse>().Execute(new Commands.RequestCommand<PositionExpectationQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();

        }

        public async Task<PositionReceivablesQueryResponse> GetPositionsWithKey(string key)
        {
            var request = new RestRequest($"{options.BaseUrl}/{PathBaseKey}/{key}", DataFormat.Json);

            return await GetPipeline<PositionReceivablesQueryResponse>().Execute(new Commands.RequestCommand<PositionReceivablesQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }

        public async Task<PositionReceivablesQueryResponse> GetPositionsWithOriginalAssetHolder(string originalAssetHolder, PositionQueryFilter paramsObj)
        {
            var pathRequest = $"{options.BaseUrl}/{PathBaseOriginalAssetHolder}";

            var queryParams = new CustomQueryParams().ReturnQueryParams(paramsObj);
            if (!string.IsNullOrEmpty(queryParams))
                pathRequest = $"{pathRequest}/{originalAssetHolder}?{queryParams}";

            var request = new RestRequest(pathRequest, DataFormat.Json);
            request.Method = Method.GET;

            return await GetPipeline<PositionReceivablesQueryResponse>().Execute(new Commands.RequestCommand<PositionReceivablesQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }

        public async Task<PositionReceivablesQueryResponse> GetPositionsWithProcessReference(string processReference)
        {

            var request = new RestRequest($"{options.BaseUrl}/{PathBaseProcessReference}/{processReference}", DataFormat.Json);

            return await GetPipeline<PositionReceivablesQueryResponse>().Execute(new Commands.RequestCommand<PositionReceivablesQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }

        public async Task<PositionReceivablesQueryResponse> GetPositionsWithReference(string reference)
        {

            var request = new RestRequest($"{options.BaseUrl}/{PathBaseReference}/{reference}", DataFormat.Json);

            return await GetPipeline<PositionReceivablesQueryResponse>().Execute(new Commands.RequestCommand<PositionReceivablesQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }
    }
}
