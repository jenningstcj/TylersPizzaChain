using System;
namespace TylersPizzaChain.Models
{
	public class PointOfSaleRequest
    { 
    public Int64 StoreId { get; set; }
    public DateTime WhenCustomerExpectsFood { get; set; }
    public bool IsPaid { get; set; }
    public PointOfSaleOrderType OrderType { get; set; }
    public Guid CustomerId { get; set; }
    public PointOfSaleDeliveryAddress? DeliveryAddress { get; set; }
    public List<PointOfSaleMenuItem> Items { get; set; } = new List<PointOfSaleMenuItem>();

}

public class PointOfSaleMenuItem
{
    public Int64 Id { get; set; }
    public Int32 Quantity { get; set; }
    public Decimal Price { get; set; }
}

public enum PointOfSaleOrderType
{
    EatIn,
    Pickup,
    Delivery
}

public class PointOfSaleDeliveryAddress
{
    public string Address1 { get; set; } = string.Empty;
    public string Address2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
}
}

