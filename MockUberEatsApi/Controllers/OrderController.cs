using Microsoft.AspNetCore.Mvc;

namespace MockUberEatsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [Route("submit")]
    public async Task<IActionResult> Post(UberEatsRequest orderRequest)
    {
        await Task.Yield();
        return new OkObjectResult(new UberEatsResponse { IsSuccess = true });
    }

    public class UberEatsResponse
    {
        public bool IsSuccess { get; set; }
    }

    public class UberEatsRequest
    {
        public Int64 StoreId { get; set; }
        public UberEatsAddress StoreAddress { get; set; } = new();
        public UberEatsAddress CustomerAddress { get; set; } = new();
        public DateTime PickupTime { get; set; }
        public DateTime WhenCustomerExpectsFood { get; set; }
        public decimal OrderAmount { get; set; }
        public List<UberEatsMenuItem> Items { get; set; } = new List<UberEatsMenuItem>();

    }

    public class UberEatsMenuItem
    {
        public Int64 Id { get; set; }
        public Int32 Quantity { get; set; }
        public Decimal Price { get; set; }
    }

    public class UberEatsAddress
    {
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
    }
}

