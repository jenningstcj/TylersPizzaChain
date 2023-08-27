using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylersPizzaChain.Database.Entities
{
	[Table("StoreHours")]
	public class StoreHours : Entity
	{
		[Key]
		public Int64 Id {get;set;}
		public Int64 StoreId { get; set; }
		public Store Store { get; set; } = new Store();

		public List<DayHours> Hours { get; set; } = new();
		public DateTime EffectiveDate { get; set; }

    }

	public class DayHours {
		[Key]
		public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
    }
}

