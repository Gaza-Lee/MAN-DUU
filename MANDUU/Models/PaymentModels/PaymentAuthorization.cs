using Newtonsoft.Json;

namespace MANDUU.Models.PaymentModels
{
    public class PaymentAuthorization
    {
        [JsonProperty("authorization_code")]
        public string AuthorizationCode { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }
    }
}