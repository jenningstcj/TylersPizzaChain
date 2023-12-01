using Microsoft.EntityFrameworkCore;
using TylersPizzaChain.Database;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Pipelines.BuildingBlocks.DatabaseQueries
{
    public static class ShoppingCartQueries
	{
        public static async Task<ShoppingCart?> GetShoppingCartById(TylersPizzaDbContext dbContext, Guid id)
        {
            return await dbContext.ShoppingCarts
                .Where(_ => _.Id == id)
                .FirstOrDefaultAsync();
        }

        public static async Task<bool> ValidateShoppingCartToStore(TylersPizzaDbContext dbContext, Guid shoppingCartId, Store store)
        {
            var shoppingCart = await GetShoppingCartById(dbContext, shoppingCartId);
            var menuItemIds = (shoppingCart?.MenuItems ?? Enumerable.Empty<MenuItemWithPrice>())
            .Select(_ => _.Id)
                .ToList();

            //get pricing tier, check prices
            var menuItemPricesAtStorePricingTier = await dbContext.Stores
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

