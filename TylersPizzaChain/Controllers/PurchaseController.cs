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
            return new BadRequestObjectResult(new { }); //fill out error object
        }
    }
}

