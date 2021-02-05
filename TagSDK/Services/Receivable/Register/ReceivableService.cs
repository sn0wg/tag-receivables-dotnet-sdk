using RestSharp;
using System;
using System.Threading.Tasks;
using TagSDK.Extensions;
using TagSDK.Models.Enums;
using TagSDK.Models.Receivable.Register;

namespace TagSDK.Services.Receivable.Register
{
    public class ReceivableService : BaseService, IReceivableService
    {
        private const string Path = Constants.Constants.Receivable.BasePath;
        private Profile baseProfile = Profile.ACQUIRER;

        public ReceivableService(IServiceProvider serviceProvider, SDKOptions options) :
            base(serviceProvider, options)
        {
        }

        public async Task<ReceivableResponse> RegisterReceivable(ReceivableRequest receivableInput)
        {
            var request = new RestRequest($"{options.BaseUrl}/{Path}", DataFormat.Json);
            request.Method = Method.POST;
            request.AddJsonBody(receivableInput);

            return await GetPipeline<ReceivableResponse>().Execute(new Commands.RequestCommand<ReceivableResponse>()
            {
                Model = receivableInput,
                RestRequest = request,
                Profile = baseProfile
            }).MapResponse();

        }
    }
}
