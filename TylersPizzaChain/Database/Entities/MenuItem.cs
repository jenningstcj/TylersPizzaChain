using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylersPizzaChain.Database.Entities
{
    [Table("MenuItem")]
    public class MenuItem : Entity
    {
        [Key]
        public Int64 Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<MenuItemPrice> MenuItemPrices { get; set; } = new List<MenuItemPrice>();
    }
}

