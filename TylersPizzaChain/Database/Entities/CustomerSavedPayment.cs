using System;
namespace TylersPizzaChain.Database.Entities
{
	public class CustomerSavedPayment : Entity
	{
		public Guid Id { get; set; }
		public Guid CustomerId { get; set; }
		public virtual Customer? Customer { get; set; }

		public string VendorPaymentId { get; set; } = string.Empty;
		public CardTypes CardType { get; set; }
		public int ExpirationMonth { get; set; }
		public int ExpirationYear { get; set; }
	}

	public enum CardTypes
	{
		Discover,
		MasterCard,
		Visa
	}
}

