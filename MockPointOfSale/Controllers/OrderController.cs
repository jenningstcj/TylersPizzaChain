using Microsoft.AspNetCore.Mvc;

namespace MockPointOfSale.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "Submit")]
    [Route("Submit")]
    public async Task<IActionResult> Post(OrderRequest orderRequest)
    {

        return new OkObjectResult(new PointOfSaleResponse { IsSuccess = true });
    }

    public class PointOfSaleResponse
    {
        public bool IsSuccess { get; set; }
    }

    public class OrderRequest
    {
        public Int64 StoreId { get; set; }
        public DateTime WhenCustomerExpectsFood { get; set; }
        public bool IsPaid { get; set; }
        public OrderType OrderType { get; set; }
        public Guid CustomerId { get; set; }
        public DeliveryAddress? DeliveryAddress { get; set; }
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();

    }

    public class MenuItem
    {
        public Int64 Id { get; set; }
        public Int32 Quantity { get; set; }
        public Decimal Price { get; set; }
    }

    public enum OrderType
    {
        EatIn,
        Pickup,
        Delivery
    }

    public class DeliveryAddress
    {
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
    }
}

