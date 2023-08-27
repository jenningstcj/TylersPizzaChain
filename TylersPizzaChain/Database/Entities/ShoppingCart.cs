using System;
using System.ComponentModel.DataAnnotations.Schema;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Database.Entities
{
	public class ShoppingCart : Entity
	{
		public Guid Id { get; set; }
		public string CartDetails { get; set; } = string.Empty; //json stringified


		[NotMapped]
		public virtual List<MenuItemWithPrice>? MenuItems { get { return System.Text.Json.JsonSerializer.Deserialize<List<MenuItemWithPrice>>(this.CartDetails); } }

	}
}
