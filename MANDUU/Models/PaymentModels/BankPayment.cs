using Newtonsoft.Json;
using System.Collections.Generic;

namespace MANDUU.Models.PaymentModels
{
    public class BankPayment
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    public class BankPaymentResponse
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public List<BankPayment> Data { get; set; } = new();
    }
}