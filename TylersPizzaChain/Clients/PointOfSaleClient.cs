using System;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Clients
{
	public interface IPointOfSaleClient
	{
        Task<PointOfSaleResponse> SendToPointOfSale(OrderDetails orderDetails, ShoppingCart shoppingCart);
	}

	public class PointOfSaleClient : IPointOfSaleClient
	{
        private readonly HttpClient _httpClient;

        public PointOfSaleClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<PointOfSaleResponse> SendToPointOfSale(OrderDetails orderDetails, ShoppingCart shoppingCart)
        {
            await Task.Yield();

            //send back fake response
            return new PointOfSaleResponse
            {
                IsSuccess = true
            };
        }
    }
}

