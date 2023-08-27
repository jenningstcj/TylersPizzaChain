using System;
using Microsoft.EntityFrameworkCore;
using TylersPizzaChain.Database;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Services
{
	public interface IShoppingCartService
	{
		Task<ShoppingCart?> GetShoppingCartById(Guid id);
		Task<bool> ValidateShoppingCartToStore(Guid shoppingCartId, Store store);

    }

	public class ShoppingCartService : IShoppingCartService
	{
		private readonly TylersPizzaDbContext _dbContext;

		public ShoppingCartService(TylersPizzaDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ShoppingCart?> GetShoppingCartById(Guid id)
		{
			return await _dbContext.ShoppingCarts
				.Where(_ => _.Id == id)
				.FirstOrDefaultAsync();
		}

        public async Task<bool> ValidateShoppingCartToStore(Guid shoppingCartId, Store store)
        {
			var shoppingCart = await GetShoppingCartById(shoppingCartId);
			var menuItemIds = (shoppingCart?.MenuItems ?? Enumerable.Empty<MenuItemWithPrice>())
				.Select(_ => _.Id)
				.ToList();

			//get pricing tier, check prices
			var menuItemPricesAtStorePricingTier = await _dbContext.Stores
				.Where(_ => _.Id == store.Id)
				.Select(_ => _.PricingTier)
				.SelectMany(_ => _.MenuItemPrices)
				.Where(_ => menuItemIds.Contains(_.MenuItemId))
				.ToDictionaryAsync(_ => _.MenuItemId, _ => _.Price);


			var areItemsValid = shoppingCart?.MenuItems?.All(mi => mi.Price == menuItemPricesAtStorePricingTier[mi.Id]
															  && mi.TaxRate == store.TaxRate) ?? false;

			return areItemsValid;
        }
    }
}

