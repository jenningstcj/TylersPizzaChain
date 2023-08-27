using System;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Services
{
	public interface IStoreHoursValidationService
	{
		bool ValidateOrderTime(OrderDetails orderDetails);
	}

	public class StoreHoursValidationService : IStoreHoursValidationService
	{
		private readonly IConfiguration _configuration;

		public StoreHoursValidationService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

        public bool ValidateOrderTime(OrderDetails orderDetails)
        {
			//check if delivery or pickup and choose correct lead time from appsettings
            throw new NotImplementedException();
        }
    }
}

