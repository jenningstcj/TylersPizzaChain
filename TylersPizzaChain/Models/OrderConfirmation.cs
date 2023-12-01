using System;
namespace TylersPizzaChain.Models
{
	public class OrderConfirmation
	{
        public bool Processed { get; set; }
        public Guid ShoppingCardId { get; set; }
        public string ConfirmationNumber { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}

