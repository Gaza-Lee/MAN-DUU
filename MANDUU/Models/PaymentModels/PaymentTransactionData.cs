using Newtonsoft.Json;

namespace MANDUU.Models.PaymentModels
{
    public class PaymentTransactionData
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("authorization_url")]
        public string AuthorizationUrl { get; set; }

        [JsonProperty("access_code")]
        public string AccessCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("authorization")]
        public PaymentAuthorization Authorization { get; set; }
    }
}