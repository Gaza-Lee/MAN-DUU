using Newtonsoft.Json;
using System.Collections.Generic;

namespace MANDUU.Models.PaymentModels
{
    public class BankPayment
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("currency")]
        public string Currency { get; set; } = string.Empty;
    }

    public class BankPaymentResponse
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("data")]
        public List<BankPayment> Data { get; set; } = new();
    }
}