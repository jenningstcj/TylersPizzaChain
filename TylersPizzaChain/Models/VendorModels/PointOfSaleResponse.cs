using System;
using System.Text.Json.Serialization;

namespace TylersPizzaChain.Models
{
	public class PointOfSaleResponse
	{
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
    }
}

