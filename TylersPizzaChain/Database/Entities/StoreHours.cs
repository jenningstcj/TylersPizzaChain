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
		public virtual Store? Store { get; set; }
		public string Hours { get; set; } = string.Empty;

		[NotMapped]//TODO: Setup for saving new hours
		public virtual List<DayHours> HoursList { get { return System.Text.Json.JsonSerializer.Deserialize<List<DayHours>>(this.Hours) ?? new(); } }
		public DateTime EffectiveDate { get; set; }

    }

	public class DayHours {
		public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
    }
}

