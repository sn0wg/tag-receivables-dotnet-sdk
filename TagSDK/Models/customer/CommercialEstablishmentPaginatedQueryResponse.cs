using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;
using TagSDK.Models.Response;

namespace TagSDK.Models.Customer
{
    public class CommercialEstablishmentPaginatedQueryResponse : BaseResponse
    {

        [JsonProperty("commercialEstablishments")]
        private List<CommercialEstablishmentQueryOutput> CommercialEstablishments { get; set; }

        [JsonProperty("pages")]
        private PagesOutput Pages { get; set; }
    }
}
