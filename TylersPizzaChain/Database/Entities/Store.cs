using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylersPizzaChain.Database.Entities
{
    [Table("Store")]
	public class Store : Entity
	{
        [Key]
		public Int64 Id { get; set; }
		public string Name { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public Int32 PricingTier { get; set; }
        public decimal TaxRate { get; set; }

        public ICollection<StoreHours> StoreHours { get; set; } = new List<StoreHours>();

    }
}

