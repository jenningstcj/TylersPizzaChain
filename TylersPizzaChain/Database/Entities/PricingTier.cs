using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylersPizzaChain.Database.Entities
{
	[Table("PricingTier")]
	public class PricingTier : Entity
	{
		[Key]
		public Int32 Id { get; set; }
		public string Name { get; set; } = string.Empty;
	}
}

