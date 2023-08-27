using Microsoft.EntityFrameworkCore;
using TylersPizzaChain.Database;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Extensions;

namespace TylersPizzaChain.Services
{
    public interface IStoreService
	{
		Task<Store?> GetStoreById(Int64 id);
		Task<StoreHours?> GetStoreHours(Int64 storeId);
    }

	public class StoreService : IStoreService
	{
        private readonly TylersPizzaDbContext _dbContext;

        public StoreService(TylersPizzaDbContext dbContext)
		{
			_dbContext = dbContext;
		}


		public async Task<Store?> GetStoreById(Int64 id)
		{
			return await _dbContext.Stores
				.Where(_ => _.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<StoreHours?> GetStoreHours(Int64 storeId)
		{
			var allHours = await _dbContext.StoreHours.ToListAsync();
			return await _dbContext.StoreHours
				.Where(_ => _.StoreId == storeId)
				.ByEffectiveDate(DateTime.UtcNow);

        }

    }
}

