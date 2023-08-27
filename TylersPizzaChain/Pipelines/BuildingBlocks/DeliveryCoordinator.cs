using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Models;
using TylersPizzaChain.Pipelines.BuildingBlocks.Clients;

namespace TylersPizzaChain.Pipelines.BuildingBlocks
{
    public static class DeliveryCoordinator
    {
        public static async Task<DeliveryResponse> SendOrderToDeliveryProvider(IHttpClientFactory httpClientFactory, OrderDetails orderDetails, ShoppingCart shoppingCart, Store store, Decimal orderTotal)
        {
            var r = new Random().Next(1, 3);
            var response = r switch
            {
                1 => await DoorDashClient.SendOrder(httpClientFactory, orderDetails, shoppingCart, store, orderTotal),
                2 => await GrubHubClient.SendOrder(httpClientFactory, orderDetails, shoppingCart, store, orderTotal),
                3 => await UberEatsClient.SendOrder(httpClientFactory, orderDetails, shoppingCart, store, orderTotal),
                _ => throw new NotImplementedException()
            };
            return response;
        }
    }
}

