using System;
namespace TylersPizzaChain.Models
{
	public class OrderDetails
	{
        public Int64 StoreId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public DateTime ExpectedOrderReadyTime { get; set; }
        public Guid PaymentId { get; set; }
        public OrderType OrderType { get; set; }
        public Guid CustomerId { get; set; }
        public DeliveryAddress? DeliveryAddress { get; set; }

    }

    public enum OrderType
    {
        EatIn,
        Pickup,
        Delivery
    }

    public class DeliveryAddress : Address
    {

    }
}

