using System;
namespace TylersPizzaChain.Models
{
	public class MenuItemWithPrice
	{
        public Int64 Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Int64 PricingTierId { get; set; }
        public decimal Price { get; set; }
        public decimal TaxRate { get; set; }
    }
}

