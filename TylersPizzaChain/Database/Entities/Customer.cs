using System;
namespace TylersPizzaChain.Database.Entities
{
	public class Customer : Entity
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;

		public List<CustomerSavedPayment> CustomerSavedPayments { get; set; } = new();
	}
}

