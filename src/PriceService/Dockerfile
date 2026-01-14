# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["PriceService.csproj", "./"]
RUN dotnet restore "PriceService.csproj"

# Copy source code and build
COPY . .
RUN dotnet build "PriceService.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "PriceService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PriceService.dll"]
