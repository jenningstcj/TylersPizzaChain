using System;
using System.Text.Json.Serialization;

namespace TylersPizzaChain.Models
{
    public class UberEatsResponse
    {
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
    }

    public class UberEatsRequest
    {
        public Int64 StoreId { get; set; }
        public UberEatsAddress StoreAddress { get; set; } = new();
        public UberEatsAddress CustomerAddress { get; set; } = new();
        public DateTime PickupTime { get; set; }
        public DateTime WhenCustomerExpectsFood { get; set; }
        public decimal OrderAmount { get; set; }
        public List<UberEatsMenuItem> Items { get; set; } = new List<UberEatsMenuItem>();

    }

    public class UberEatsMenuItem
    {
        public Int64 Id { get; set; }
        public Int32 Quantity { get; set; }
        public Decimal Price { get; set; }
    }

    public class UberEatsAddress
    {
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
    }
}

