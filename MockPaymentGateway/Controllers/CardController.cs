using Microsoft.AspNetCore.Mvc;

namespace MockPaymentGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController : ControllerBase
{
    

    private readonly ILogger<CardController> _logger;

    public CardController(ILogger<CardController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "Charge")]
    [Route("charge")]
    public async Task<IActionResult> Post([FromBody] ChargeRequest chargeRequest)
    {
        await Task.Yield();
        return new OkObjectResult(new ChargeResponse
        {
            AuthorizationNumber = Guid.NewGuid().ToString(),
            ReferenceNumber = Guid.NewGuid().ToString(),
            AmountInCents = chargeRequest.AmountInCents,
            IsSuccess = true
        });
    }

    public class ChargeRequest
    {
        public string PaymentId { get; set; } = string.Empty;
        public Decimal AmountInCents { get; set; }
    }

    public class ChargeResponse
    {
        public string AuthorizationNumber { get; set; } = string.Empty;
        public string ReferenceNumber { get; set; } = string.Empty;
        public decimal AmountInCents { get; set; }
        public bool IsSuccess { get; set; }

    }
}

