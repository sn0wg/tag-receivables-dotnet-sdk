using RestSharp;
using System;
using System.Threading.Tasks;
using TagSDK.Extensions;
using TagSDK.Models.Enums;
using TagSDK.Models.Receivable.Contract;
using TagSDK.Models.Request;
using TagSDK.Utils;

namespace TagSDK.Services.Receivable.Contract
{
    public class ContractService : BaseService, IContractService
    {
        private const string PathBase = Constants.Constants.Endorsement.Contract.BasePath;
        private const string PathBaseHistory = Constants.Constants.Endorsement.Contract.BasePathHistory;
        private const string PathBaseKey = Constants.Constants.Endorsement.Contract.BasePathKey;
        private const string PathBaseProcessKey = Constants.Constants.Endorsement.Contract.BasePathProcessKey;
        private const string PathBaseReference = Constants.Constants.Endorsement.Contract.BasePathReference;
        private Profile baseProfile = Profile.CREDITOR;
        public ContractService(IServiceProvider serviceProvider, SDKOptions options) :
            base(serviceProvider, options)
        {
        }

        public async Task<ContractResponse> InsertContract(ContractRequest contract)
        {
            var request = new RestRequest($"{options.BaseUrl}/{PathBase}", DataFormat.Json);
            request.Method = Method.POST;
            request.AddJsonBody(contract);

            return await GetPipeline<ContractResponse>().Execute(new Commands.RequestCommand<ContractResponse>()
            {
                Model = contract,
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }
        public async Task<ContractPaginatedQueryResponse> ListContractsByReference(string reference, Pagination page)
        {
            var path = $"{options.BaseUrl}/{PathBaseReference}/{reference}";
            var queryParams = new CustomQueryParams().ReturnQueryParams(page);
            var request = new RestRequest($"{path}{queryParams}", DataFormat.Json);
            request.Method = Method.GET;

            return await GetPipeline<ContractPaginatedQueryResponse>().Execute(new Commands.RequestCommand<ContractPaginatedQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }

        public async Task<ContractPaginatedQueryResponse> ListContractsByKey(string key)
        {
            var request = new RestRequest($"{options.BaseUrl}/{PathBaseKey}/{key}", DataFormat.Json);
            request.Method = Method.GET;

            return await GetPipeline<ContractPaginatedQueryResponse>().Execute(new Commands.RequestCommand<ContractPaginatedQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }

        public async Task<ContractPaginatedQueryResponse> ListContractsByProcessKey(string processKey, Pagination page)
        {
            var path = $"{options.BaseUrl}/{ PathBaseProcessKey}/{processKey}";
            var queryParams = new CustomQueryParams().ReturnQueryParams(page);
            var request = new RestRequest($"{path}{queryParams}", DataFormat.Json);
            request.Method = Method.GET;

            return await GetPipeline<ContractPaginatedQueryResponse>().Execute(new Commands.RequestCommand<ContractPaginatedQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }

        public async Task<ContractPaginatedQueryResponse> ListContractsWithParams(ContractQueryFilter contractFilter, Pagination page)
        {
            var pathRequest = $"{options.BaseUrl}/{PathBase}";

            var queryParams = new CustomQueryParams().ReturnQueryParams(contractFilter, page);
            if (!string.IsNullOrEmpty(queryParams))
                pathRequest = $"{pathRequest}{queryParams}";

            var request = new RestRequest(pathRequest, DataFormat.Json);



            if (!string.IsNullOrEmpty(queryParams))
                pathRequest = $"{pathRequest}{queryParams}";

            return await GetPipeline<ContractPaginatedQueryResponse>().Execute(new Commands.RequestCommand<ContractPaginatedQueryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }
        public async Task<ContractHistoryResponse> GetContractHistoryWithKey(string key)
        {
            //setToken(AuthType.CREDITOR);

            var request = new RestRequest($"{options.BaseUrl}/{PathBaseHistory}?key={key}", DataFormat.Json);
            return await GetPipeline<ContractHistoryResponse>().Execute(new Commands.RequestCommand<ContractHistoryResponse>()
            {
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();
        }
    }
}




