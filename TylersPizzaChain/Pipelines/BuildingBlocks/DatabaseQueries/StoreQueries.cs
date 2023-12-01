using Microsoft.EntityFrameworkCore;
using TylersPizzaChain.Database;
using TylersPizzaChain.Database.Entities;

namespace TylersPizzaChain.Pipelines.BuildingBlocks.DatabaseQueries
{
    public static class StoreQueries
	{
        public static async Task<Store?> GetStoreById(TylersPizzaDbContext dbContext, Int64 id)
            =>await dbContext.Stores
                .ByStoreId(id)
                .FirstOrDefaultAsync();
        

        public static async Task<StoreHours?> GetStoreHours(TylersPizzaDbContext dbContext, Int64 storeId)
            => await dbContext.StoreHours
                .ByStoreId(storeId)
                .ByEffectiveDate(DateTime.UtcNow);

    }
}

