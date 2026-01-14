using Microsoft.AspNetCore.Mvc;
using AlertService.Models;

namespace AlertService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    private readonly ILogger<AlertsController> _logger;
    // TODO: Add in-memory storage for alerts
    // Example: private static readonly List<Alert> _alerts = new();

    public AlertsController(ILogger<AlertsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult CreateAlert([FromBody] Alert alert)
    {
        // TODO: Implement alert creation logic
        // - Generate new GUID for Id
        // - Set CreatedAt timestamp
        // - Store in in-memory collection
        // - Return created alert with 201 status
        throw new NotImplementedException();
    }

    [HttpGet]
    public IActionResult GetAllAlerts()
    {
        // TODO: Implement logic to return all alerts
        throw new NotImplementedException();
    }

    [HttpGet("{symbol}")]
    public IActionResult GetAlertsBySymbol(string symbol)
    {
        // TODO: Implement logic to filter alerts by symbol
        throw new NotImplementedException();
    }
}
