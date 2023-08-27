using System.Text;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Pipelines.BuildingBlocks.Clients
{
    public static class UberEatsClient
	{
        public static async Task<DeliveryResponse> SendOrder(IHttpClientFactory httpClientFactory, OrderDetails orderDetails, ShoppingCart shoppingCart, Store store, Decimal orderTotal)
        {
            var httpClient = httpClientFactory.CreateClient("UberEatsClient");

            var httpContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(new UberEatsRequest
                {
                    CustomerAddress = new UberEatsAddress
                    {
                        Address1 = orderDetails.DeliveryAddress!.Address1,
                        Address2 = orderDetails.DeliveryAddress.Address2,
                        City = orderDetails.DeliveryAddress.City,
                        State = orderDetails.DeliveryAddress.State,
                        Zip = orderDetails.DeliveryAddress.Zip
                    },
                    OrderAmount = orderTotal,
                    StoreAddress = new UberEatsAddress
                    {
                        Address1 = store.Address1,
                        Address2 = store.Address2,
                        City = store.City,
                        State = store.State,
                        Zip = store.Zip
                    },
                    PickupTime = orderDetails.WhenCustomerExpectsFood.AddMinutes(-15),
                    StoreId = orderDetails.StoreId,
                    WhenCustomerExpectsFood = orderDetails.WhenCustomerExpectsFood,
                    Items = (shoppingCart.MenuItems ?? Enumerable.Empty<MenuItemWithPrice>())
                    .Select(_ => new UberEatsMenuItem
                    {
                        Id = _.Id,
                        Price = _.Price,
                        Quantity = 1
                    }).ToList()
                }),
                UnicodeEncoding.UTF8,
                "application/json"
                );
            var httpResponse = await httpClient.PostAsync("/order/submit", httpContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseData = System.Text.Json.JsonSerializer.Deserialize<UberEatsResponse>(await httpResponse.Content.ReadAsStringAsync());
                return new DeliveryResponse() { IsSuccess = responseData?.IsSuccess ?? false };
            }
            else
            {
                return new DeliveryResponse() { IsSuccess = false };
            }
        }
    }
}

