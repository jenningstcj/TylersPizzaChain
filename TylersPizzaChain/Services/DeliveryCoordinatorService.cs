using System;
using TylersPizzaChain.Clients;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Services
{
    public interface IDeliveryCoordinatorService
    {
        Task<DeliveryResponse> SendOrderToDeliveryProvider(OrderDetails orderDetails, ShoppingCart shoppingCart);
    }

    public class DeliveryCoordinatorService : IDeliveryCoordinatorService
    {
        private readonly IDoorDashClient _doorDashClient;
        private readonly IGrubHubClient _grubHubClient;
        private readonly IUberEatsClient _uberEatsClient;

        public DeliveryCoordinatorService(IDoorDashClient doorDashClient,
            IGrubHubClient grubHubClient,
            IUberEatsClient uberEatsClient)
        {
            _doorDashClient = doorDashClient;
            _grubHubClient = grubHubClient;
            _uberEatsClient = uberEatsClient;
        }

        public async Task<DeliveryResponse> SendOrderToDeliveryProvider(OrderDetails orderDetails, ShoppingCart shoppingCart)
        {
            var r = new Random().Next(1, 3);
            var response = r switch
            {
                1 => await _doorDashClient.SendOrder(orderDetails, shoppingCart),
                2 => await _grubHubClient.SendOrder(orderDetails, shoppingCart),
                3 => await _uberEatsClient.SendOrder(orderDetails, shoppingCart),
                _ => throw new NotImplementedException()
            };
            return response;
        }
    }
}

