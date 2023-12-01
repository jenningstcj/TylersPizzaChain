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
		public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

		public Int32 PricingTierId { get; set; }
		public virtual PricingTier? PricingTier { get; set; }

		public Decimal Price { get; set; }
		public DateTime EffectiveDate { get; set; }
	}
}

