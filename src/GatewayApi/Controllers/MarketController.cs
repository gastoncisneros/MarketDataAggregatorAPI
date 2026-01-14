using Microsoft.AspNetCore.Mvc;
using GatewayApi.Services;

namespace GatewayApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarketController : ControllerBase
{
    private readonly ILogger<MarketController> _logger;
    private readonly IPriceServiceClient _priceServiceClient;

    public MarketController(
        ILogger<MarketController> logger,
        IPriceServiceClient priceServiceClient)
    {
        _logger = logger;
        _priceServiceClient = priceServiceClient;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        // TODO: Implement market summary aggregation logic
        throw new NotImplementedException();
    }
}
