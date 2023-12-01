using Microsoft.AspNetCore.Mvc;

namespace MockDoorDashClient.Controllers;

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
    public async Task<IActionResult> Post(DoorDashRequest orderRequest)
    {
        await Task.Yield();
        return new OkObjectResult(new DoorDashResponse {IsSuccess = true });
    }

    public class DoorDashResponse
    {
        public bool IsSuccess { get; set; }
    }

    public class DoorDashRequest
    {
        public Int64 StoreId { get; set; }
        public DoorDashAddress StoreAddress { get; set; } = new();
        public DoorDashAddress CustomerAddress { get; set; } = new();
        public DateTime PickupTime { get; set; }
        public DateTime WhenCustomerExpectsFood { get; set; }
        public decimal OrderAmount { get; set; }
        public List<DoorDashMenuItem> Items { get; set; } = new List<DoorDashMenuItem>();

    }

    public class DoorDashMenuItem
    {
        public Int64 Id { get; set; }
        public Int32 Quantity { get; set; }
        public Decimal Price { get; set; }
    }

    public class DoorDashAddress
    {
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
    }
}

