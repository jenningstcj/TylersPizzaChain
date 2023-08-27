using System;
namespace TylersPizzaChain.Database.Entities
{
	public class CustomerSavedPayment : Entity
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public Customer Customer { get; set; } = new();

		public string VendorPaymentId { get; set; } = string.Empty;
		public string CardType { get; set; } = string.Empty;
		public int ExpirationMonth { get; set; }
		public int ExpirationYear { get; set; }
	}
}

