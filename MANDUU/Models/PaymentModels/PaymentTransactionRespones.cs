using Newtonsoft.Json;

namespace MANDUU.Models.PaymentModels
{
    public class PaymentTransactionResponse
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("data")]
        public PaymentTransactionData Data { get; set; } = null!;
    }
}