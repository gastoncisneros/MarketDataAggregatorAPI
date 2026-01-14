using GatewayApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: Configure CORS policy
// Example: builder.Services.AddCors(options => { ... });

// TODO: Configure HttpClients for service-to-service communication
// Example:
// builder.Services.AddHttpClient<IPriceServiceClient, PriceServiceClient>(client =>
// {
//     client.BaseAddress = new Uri(builder.Configuration["PriceServiceUrl"] ?? "http://price-service:8081");
//     client.Timeout = TimeSpan.FromSeconds(30);
// });

// Example:
// builder.Services.AddHttpClient<IAlertServiceClient, AlertServiceClient>(client =>
// {
//     client.BaseAddress = new Uri(builder.Configuration["AlertServiceUrl"] ?? "http://alert-service:8082");
//     client.Timeout = TimeSpan.FromSeconds(30);
// });

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TODO: Add CORS middleware
// Example: app.UseCors("AllowAll");

app.MapControllers();

app.Run();
