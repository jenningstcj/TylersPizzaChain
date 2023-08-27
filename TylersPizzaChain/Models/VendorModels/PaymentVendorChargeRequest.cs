using System;
namespace TylersPizzaChain.Models
{
    public class PaymentVendorChargeRequest
    {
        public string PaymentId { get; set; } = string.Empty;
        public Decimal AmountInCents { get; set; }
    }
}

