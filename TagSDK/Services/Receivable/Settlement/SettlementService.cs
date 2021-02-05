using System;
using RestSharp;
using System.Threading.Tasks;
using TagSDK.Extensions;
using TagSDK.Utils;
using TagSDK.Models.Receivable.Settlement;
using TagSDK.Models.Request;
using TagSDK.Models.Response;
using TagSDK.Models.Enums;

namespace TagSDK.Services.Receivable.Settlement
{
    public class SettlementService : BaseService, ISettlementService
    {
        private const string Path = Constants.Constants.Settlement.BasePath;
        private const string PathReject = Constants.Constants.Settlement.BasePathReject;
        private const string PathKey = Constants.Constants.Settlement.BasePathKey;
        private const string PathProcessKey = Constants.Constants.Settlement.BasePathProcessKey;
        private const string PathReference = Constants.Constants.Settlement.BasePathReference;
        private Profile baseProfile = Profile.ACQUIRER;

        public SettlementService(IServiceProvider serviceProvider, SDKOptions options) :
            base(serviceProvider, options)
        {
        }

        public async Task<SettlementPaginatedQueryResponse> GetSettlementWithParams(SettlementQueryFilter settlementParams, Pagination pagination)
        {

            var pathRequest = $"{options.BaseUrl}/{Path}";
            var queryParams = new CustomQueryParams().ReturnQueryParams(settlementParams, pagination);

            if (!string.IsNullOrEmpty(queryParams))
                pathRequest = $"{pathRequest}/{queryParams}";

            var request = new RestRequest(pathRequest, DataFormat.Json);

            return await GetPipeline<SettlementPaginatedQueryResponse>().Execute(new Commands.RequestCommand<SettlementPaginatedQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }

        public async Task<SettlementQueryResponse> GetSettlementByKey(string key)
        {
            var request = new RestRequest($"{options.BaseUrl}/{PathKey}/{key}", DataFormat.Json);

            return await GetPipeline<SettlementQueryResponse>().Execute(new Commands.RequestCommand<SettlementQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }

        public async Task<SettlementPaginatedQueryResponse> GetSettlementByProcessKey(string processKey, Pagination pagination)
        {
            string queryParams = $"?processkey={processKey}";
            queryParams = new CustomQueryParams().ReturnQueryParams(queryParams, pagination);

            var request = new RestRequest($"{options.BaseUrl}/{PathProcessKey}{queryParams}", DataFormat.Json);

            return await GetPipeline<SettlementPaginatedQueryResponse>().Execute(new Commands.RequestCommand<SettlementPaginatedQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }

        public async Task<SettlementPaginatedQueryResponse> GetSettlementByReference(string reference, Pagination pagination)
        {
            string queryParams = $"?reference={reference}";
            queryParams = new CustomQueryParams().ReturnQueryParams(queryParams, pagination);

            var request = new RestRequest($"{options.BaseUrl}/{PathReference}{queryParams}", DataFormat.Json);

            return await GetPipeline<SettlementPaginatedQueryResponse>().Execute(new Commands.RequestCommand<SettlementPaginatedQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }

        public async Task<SettlementResponse> ReportSettlement(SettlementRequest receivableSettlement)
        {
            var request = new RestRequest($"{options.BaseUrl}/{Path}", DataFormat.Json);
            request.Method = Method.PATCH;
            request.AddJsonBody(receivableSettlement);

            return await GetPipeline<SettlementResponse>().Execute(new Commands.RequestCommand<SettlementResponse>()
            {
                Model = receivableSettlement,
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }

        public async Task<BaseResponse> RejectSettlement(SettlementRejectRequest receivableSettlement)
        {
            var request = new RestRequest($"{options.BaseUrl}/{PathReject}", DataFormat.Json);
            request.Method = Method.PATCH;
            request.AddJsonBody(receivableSettlement);

            return await GetPipeline<BaseResponse>().Execute(new Commands.RequestCommand<BaseResponse>()
            {
                Model = receivableSettlement,
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }


    }
}
