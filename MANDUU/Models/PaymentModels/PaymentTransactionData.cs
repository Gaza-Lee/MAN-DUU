using Newtonsoft.Json;

namespace MANDUU.Models.PaymentModels
{
    public class PaymentTransactionData
    {
        [JsonProperty("reference")]
        public string Reference { get; set; } = string.Empty;

        [JsonProperty("authorization_url")]
        public string AuthorizationUrl { get; set; } = string.Empty;

        [JsonProperty("access_code")]
        public string AccessCode { get; set; } = string.Empty;

        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;

        [JsonProperty("authorization")]
        public PaymentAuthorization Authorization { get; set; } = null!;
    }
}