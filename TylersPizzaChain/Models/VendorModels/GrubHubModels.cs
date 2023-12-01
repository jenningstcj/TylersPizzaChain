﻿using System;
using System.Text.Json.Serialization;

namespace TylersPizzaChain.Models
{
    public class GrubHubResponse
    {
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
    }

    public class GrubHubRequest
    {
        public Int64 StoreId { get; set; }
        public GrubHubAddress StoreAddress { get; set; } = new();
        public GrubHubAddress CustomerAddress { get; set; } = new();
        public DateTime PickupTime { get; set; }
        public DateTime WhenCustomerExpectsFood { get; set; }
        public decimal OrderAmount { get; set; }
        public List<GrubHubMenuItem> Items { get; set; } = new List<GrubHubMenuItem>();

    }

    public class GrubHubMenuItem
    {
        public Int64 Id { get; set; }
        public Int32 Quantity { get; set; }
        public Decimal Price { get; set; }
    }

    public class GrubHubAddress
    {
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
    }
}

