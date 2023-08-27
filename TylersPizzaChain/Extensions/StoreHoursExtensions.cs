using System;
using Microsoft.EntityFrameworkCore;
using TylersPizzaChain.Database.Entities;

namespace TylersPizzaChain.Extensions
{
	public static class StoreHoursExtensions
	{
		public static async Task<StoreHours?> ByEffectiveDate(this IQueryable<StoreHours> storeHours, DateTime effectiveDate)
		{
			return await storeHours
				.Where(_ => _.EffectiveDate <= effectiveDate)
				.OrderByDescending(_ => _.EffectiveDate)
				.FirstOrDefaultAsync();
		}
	}
}

