using Microsoft.AspNetCore.Mvc;
using GatewayApi.Services;

namespace GatewayApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    private readonly ILogger<AlertsController> _logger;
    private readonly IAlertServiceClient _alertServiceClient;

    public AlertsController(
        ILogger<AlertsController> logger,
        IAlertServiceClient alertServiceClient)
    {
        _logger = logger;
        _alertServiceClient = alertServiceClient;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAlert([FromBody] object alertRequest)
    {
        // TODO: Implement create alert logic (proxy to Alert Service)
        throw new NotImplementedException();
    }

    [HttpGet("{symbol}")]
    public async Task<IActionResult> GetAlerts(string symbol)
    {
        // TODO: Implement get alerts logic (proxy to Alert Service)
        throw new NotImplementedException();
    }
}
