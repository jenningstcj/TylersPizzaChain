using Microsoft.AspNetCore.Mvc;
using TylersPizzaChain.Exceptions;
using TylersPizzaChain.Models;
using TylersPizzaChain.Services;

namespace TylersPizzaChain.Controllers;

[ApiController]
[Route("[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly ILogger<PurchaseController> _logger;
    private readonly IOrderService _orderService;

    public PurchaseController(ILogger<PurchaseController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    /*
     * {
  "storeId": 1,
  "shoppingCartId": "ada109ae-7244-496c-80cd-0c7bc7f3abcc",
  "expectedOrderReadyTime": "2023-08-27T00:43:32.239Z",
  "paymentId": "3e7a1033-4543-47d7-b3c7-62dbe3d79751",
  "orderType": 2,
  "customerId": "825a065b-3620-42af-bea2-935be9524f5c"
}
     */
    [HttpPost(Name = "Purchase")]
    public async Task<IActionResult> Post(OrderDetails orderDetails)
    {
        try
        {
            var confirmation = await _orderService.ProcessOrder(orderDetails);

            return new OkObjectResult(confirmation);
        }
        catch(OrderProcessingException ex)
        {
            return new BadRequestObjectResult(new { Message = ex.Message }); //fill out error object
        }
    }
}

