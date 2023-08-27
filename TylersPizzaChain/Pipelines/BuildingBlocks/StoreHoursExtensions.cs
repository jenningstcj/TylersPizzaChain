using Microsoft.EntityFrameworkCore;
using TylersPizzaChain.Database.Entities;

namespace TylersPizzaChain.Pipelines.BuildingBlocks
{
    public static class StoreHoursExtensions
    {
        public static async Task<StoreHours?> ByEffectiveDate(this IQueryable<StoreHours> storeHours, DateTime effectiveDate)
            => await storeHours
                .Where(_ => _.EffectiveDate <= effectiveDate)
                .OrderByDescending(_ => _.EffectiveDate)
                .FirstOrDefaultAsync();

        public static IQueryable<StoreHours> ByStoreId(this IQueryable<StoreHours> storeHours, Int64 storeId)
            => storeHours
                .Where(_ => _.StoreId == storeId);
        
    }
}

