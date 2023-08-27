using System.Text;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Exceptions;
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
            var httpContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(new PointOfSaleRequest {
                    DeliveryAddress = orderDetails.DeliveryAddress == null
                    ? null
                    : MapDeliveryAddress(orderDetails.DeliveryAddress),
                    CustomerId = orderDetails.CustomerId,
                    IsPaid = true,
                    OrderType = orderDetails.OrderType switch {
                        OrderType.Delivery => PointOfSaleOrderType.Delivery,
                        OrderType.EatIn => PointOfSaleOrderType.EatIn,
                        OrderType.Pickup => PointOfSaleOrderType.Pickup,
                        _ => throw new OrderProcessingException("Unsupported Order Type")
                        },
                    StoreId = orderDetails.StoreId,
                    WhenCustomerExpectsFood = orderDetails.WhenCustomerExpectsFood,
                    Items = shoppingCart.MenuItems.Select(_ => new PointOfSaleMenuItem
                    {
                        Id = _.Id,
                        Price = _.Price,
                        Quantity = 1
                    }).ToList()
                }),
                UnicodeEncoding.UTF8,
                "application/json"
                );
            var httpResponse = await _httpClient.PostAsync("/order/submit", httpContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseData = System.Text.Json.JsonSerializer.Deserialize<PointOfSaleResponse>(await httpResponse.Content.ReadAsStringAsync());
                return new PointOfSaleResponse() { IsSuccess = true };
            }
            else
            {
                return new PointOfSaleResponse() { IsSuccess = false };
            }
        }

        private PointOfSaleDeliveryAddress MapDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            return new PointOfSaleDeliveryAddress
            {
                Address1 = deliveryAddress.Address1,
                Address2 = deliveryAddress.Address2,
                City = deliveryAddress.City,
                State = deliveryAddress.State,
                Zip = deliveryAddress.Zip
            };
        }
    }
}

