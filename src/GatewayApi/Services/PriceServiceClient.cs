namespace GatewayApi.Services;

public class PriceServiceClient : IPriceServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PriceServiceClient> _logger;

    public PriceServiceClient(
        HttpClient httpClient,
        ILogger<PriceServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    // TODO: Implement methods to communicate with Price Service via HTTP
    // Example:
    // public async Task<PriceData> GetPriceAsync(string symbol)
    // {
    //     var response = await _httpClient.GetAsync($"/api/prices/{symbol}");
    //     response.EnsureSuccessStatusCode();
    //     return await response.Content.ReadFromJsonAsync<PriceData>();
    // }
}
