using Microsoft.AspNetCore.Mvc;

namespace PriceService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PricesController : ControllerBase
{
    private readonly ILogger<PricesController> _logger;

    public PricesController(ILogger<PricesController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{symbol}")]
    public IActionResult GetPrice(string symbol)
    {
        // TODO: Implement mock price generation for a single symbol
        // Response should include: symbol, price, timestamp, change
        throw new NotImplementedException();
    }

    [HttpGet("batch")]
    public IActionResult GetBatchPrices()
    {
        // TODO: Implement batch price retrieval for multiple symbols (AAPL, MSFT, BTC)
        // Return array of price data
        throw new NotImplementedException();
    }
}
