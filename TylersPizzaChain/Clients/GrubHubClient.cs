using System;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Clients
{
	public interface IGrubHubClient
	{
        Task<DeliveryResponse> SendOrder(OrderDetails orderDetails, ShoppingCart shoppingCart);
	}

	public class GrubHubClient : IGrubHubClient
	{
        private readonly HttpClient _httpClient;

        public GrubHubClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<DeliveryResponse> SendOrder(OrderDetails orderDetails, ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}

