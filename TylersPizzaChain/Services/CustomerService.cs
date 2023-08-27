using System;
using Microsoft.EntityFrameworkCore;
using TylersPizzaChain.Database;
using TylersPizzaChain.Database.Entities;

namespace TylersPizzaChain.Services
{
	public interface ICustomerService
	{
		Task<CustomerSavedPayment?> GetCustomerSavedPayment(Guid customerId, Guid savedPaymentId);
	}
	public class CustomerService : ICustomerService
	{
        private readonly TylersPizzaDbContext _dbContext;

        public CustomerService(TylersPizzaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerSavedPayment?> GetCustomerSavedPayment(Guid customerId, Guid savedPaymentId)
        {
            return await _dbContext
                .CustomerSavedPayments
                .Where(_ => _.CustomerId == customerId && _.Id == savedPaymentId)
                .FirstOrDefaultAsync();
        }
    }
}

