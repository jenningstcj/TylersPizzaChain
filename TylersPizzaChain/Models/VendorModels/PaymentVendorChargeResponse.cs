using System;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TylersPizzaChain.Models
{
    public class PaymentVendorChargeResponse
    {
        [JsonPropertyName("authorizationNumber")]
        public string AuthorizationNumber { get; set; } = string.Empty;
        [JsonPropertyName("referenceNumber")]
        public string ReferenceNumber { get; set; } = string.Empty;
        [JsonPropertyName("amountInCents")]
        public decimal AmountInCents { get; set; }
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
    }
}

