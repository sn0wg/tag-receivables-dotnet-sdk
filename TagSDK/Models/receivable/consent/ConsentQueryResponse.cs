using Newtonsoft.Json;
using TagSDK.Models.Response;

namespace TagSDK.Models.Receivable.Consent
{
    public class ConsentQueryResponse
    {
        [JsonProperty("optIns")]
        public ConsentQueryOutput OptIns { get; set; }
    }
}
