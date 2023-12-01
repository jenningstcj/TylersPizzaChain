using Microsoft.EntityFrameworkCore;
using TylersPizzaChain.Database;
using TylersPizzaChain.Database.Entities;

namespace TylersPizzaChain.Pipelines.BuildingBlocks.DatabaseQueries
{
    public static class CustomerQueries
	{
        public static async Task<CustomerSavedPayment?> GetCustomerSavedPayment(TylersPizzaDbContext dbContext, Guid customerId, Guid savedPaymentId)
        {
            return await dbContext
                .CustomerSavedPayments
                .Where(_ => _.CustomerId == customerId && _.Id == savedPaymentId)
                .FirstOrDefaultAsync();
        }
    }
}

