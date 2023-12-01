using TylersPizzaChain.Database.Entities;

namespace TylersPizzaChain.Pipelines.BuildingBlocks
{
    public static class StoreExtensions
	{
        public static IQueryable<Store> ByStoreId(this IQueryable<Store> storeHours, Int64 storeId)
            => storeHours
                .Where(_ => _.Id == storeId);
    }
}

