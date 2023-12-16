using TylersPizzaChain.Database.Entities;

namespace TylersPizzaChain.Models
{
    public class OrderForProcessing
	{
		public OrderForProcessing()
		{
		}


		public Store? Store { get; set; }
		public StoreHours? StoreHours { get; set; }
		public CustomerSavedPayment? CustomerSavedPayment { get; set; }
		public ShoppingCart? ShoppingCart { get; set; }

		public Decimal TotalTax { get { return Math.Round(this.ShoppingCart?.MenuItems?.Aggregate(0M, (acc, val) => acc + (val.Price * val.TaxRate)) ?? 0, 2); } }
		public Decimal Subtotal { get { return this.ShoppingCart?.MenuItems?.Aggregate(0M, (acc, val) => acc + val.Price) ?? 0; } }
		public Decimal OrderTotal { get { return this.TotalTax + this.Subtotal; } }
	}
}

