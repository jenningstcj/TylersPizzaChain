using System;
namespace TylersPizzaChain.Models
{
	public class OrderDetails
	{
        /// <summary>
        /// Store Id
        /// </summary>
        /// <example>1</example>
        public Int64 StoreId { get; set; }

        /// <summary>
        /// Expected order ready time
        /// </summary>
        /// <example>ada109ae-7244-496c-80cd-0c7bc7f3abcc</example>
        public Guid ShoppingCartId { get; set; }
        public DateTime WhenCustomerExpectsFood { get; set; }

        /// <summary>
        /// Payment Id
        /// </summary>
        /// <example>3e7a1033-4543-47d7-b3c7-62dbe3d79751</example>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Order Type
        /// </summary>
        /// <example>1</example>
        public OrderType OrderType { get; set; }

        /// <summary>
        /// Customer Id
        /// </summary>
        /// <example>825a065b-3620-42af-bea2-935be9524f5c</example>
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

