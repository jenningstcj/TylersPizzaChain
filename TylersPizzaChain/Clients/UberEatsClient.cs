using System;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Clients
{
	public interface IUberEatsClient
	{
        Task<DeliveryResponse> SendOrder(OrderDetails orderDetails, ShoppingCart shoppingCart);
    }

	public class UberEatsClient : IUberEatsClient
	{
		private readonly HttpClient _httpClient;

		public UberEatsClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

        public Task<DeliveryResponse> SendOrder(OrderDetails orderDetails, ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}

