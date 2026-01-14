namespace GatewayApi.Services;

public class AlertServiceClient : IAlertServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AlertServiceClient> _logger;

    public AlertServiceClient(
        HttpClient httpClient,
        ILogger<AlertServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    // TODO: Implement methods to communicate with Alert Service via HTTP
    // Example:
    // public async Task<Alert> CreateAlertAsync(AlertRequest request)
    // {
    //     var response = await _httpClient.PostAsJsonAsync("/api/alerts", request);
    //     response.EnsureSuccessStatusCode();
    //     return await response.Content.ReadFromJsonAsync<Alert>();
    // }
}
