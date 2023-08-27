using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylersPizzaChain.Database.Entities
{
	[Table("MenuItemPrice")]
	public class MenuItemPrice : Entity
	{
		[Key]
		public Int64 Id { get; set; }
		public Int64 MenuItemId { get; set; }
		public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

		public Int32 PricingTierId { get; set; }
		public ICollection<PricingTier> PricingTiers { get; set; } = new List<PricingTier>();

		public Decimal Price { get; set; }
		public DateTime EffectiveDate { get; set; }
	}
}

