using System;
namespace TylersPizzaChain.Database.Entities
{
	public abstract class Entity
	{
		public DateTime CreatedAt { get; set; }
		public DateTime LastUpdatedAt { get; set; }
		public Guid LastUpdatedBy { get; set; }
	}
}

