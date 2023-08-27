using System;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Clients
{
	public interface IDoorDashClient
	{
        Task<DeliveryResponse> SendOrder(OrderDetails orderDetails, ShoppingCart shoppingCart);
    }

	public class DoorDashClient : IDoorDashClient
	{
        private readonly HttpClient _httpClient;

        public DoorDashClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<DeliveryResponse> SendOrder(OrderDetails orderDetails, ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}

