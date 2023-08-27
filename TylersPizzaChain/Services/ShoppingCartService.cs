using System;
using Microsoft.EntityFrameworkCore;
using TylersPizzaChain.Database;
using TylersPizzaChain.Database.Entities;

namespace TylersPizzaChain.Services
{
	public interface IShoppingCartService
	{
		Task<ShoppingCart?> GetShoppingCartById(Guid id);
		Task<bool> ValidateShoppingCartToStore(Guid shoppingCartId, Int64 storeId);

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

        public async Task<bool> ValidateShoppingCartToStore(Guid shoppingCartId, long storeId)
        {
			var shoppingCart = await GetShoppingCartById(shoppingCartId);
			//get pricing tier, check prices
            throw new NotImplementedException();
        }
    }
}

