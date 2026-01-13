# Market Data Aggregator API

A cloud-native microservices architecture built with .NET 8 and deployed on Azure Kubernetes Service (AKS) for aggregating financial market data.

## 🎯 Project Overview

This project demonstrates a production-ready microservices architecture using modern cloud technologies. It simulates a market data aggregation system with three independent services communicating through Kubernetes service discovery.

**Purpose**: Learning and demonstrating expertise in:
- Microservices architecture
- Docker containerization
- Kubernetes orchestration
- Azure Container Registry (ACR)
- Azure Kubernetes Service (AKS)
- Service-to-service communication
- Cloud-native application design

## 🏗️ Architecture

```
                    [INGRESS / LoadBalancer]
                              |
              +---------------+----------------+
              |               |                |
         [Gateway API]   [Price Service]  [Alert Service]
              |               |                |
          Port 8080       Port 8081        Port 8082
              |               |                |
         LoadBalancer     ClusterIP        ClusterIP
```

### Communication Flow:
1. External clients → Gateway API (public endpoint)
2. Gateway API → Price Service (internal)
3. Gateway API → Alert Service (internal)
4. Services communicate via Kubernetes DNS and ClusterIP services

## 📦 Microservices

### 1. Gateway API (Port 8080)
**Role**: API Gateway, routing, and aggregation layer

**Endpoints**:
- `GET /api/health` - Health check
- `GET /api/market/summary` - Aggregates data from Price Service
- `POST /api/alerts` - Creates price alerts (proxies to Alert Service)
- `GET /api/alerts/{symbol}` - Lists alerts for a specific symbol

**Key Features**:
- HttpClient-based service discovery
- Timeout handling and resilience
- CORS enabled
- Aggregation logic

### 2. Price Service (Port 8081)
**Role**: Mock financial market data provider

**Endpoints**:
- `GET /api/prices/{symbol}` - Returns simulated price for a symbol
- `GET /api/prices/batch` - Returns multiple symbols (AAPL, MSFT, BTC)

**Response Example**:
```json
{
  "symbol": "AAPL",
  "price": 178.50,
  "timestamp": "2025-01-12T10:30:00Z",
  "change": "+2.3%"
}
```

### 3. Alert Service (Port 8082)
**Role**: Price alert management

**Endpoints**:
- `POST /api/alerts` - Create new alert (in-memory storage)
- `GET /api/alerts` - List all alerts
- `GET /api/alerts/{symbol}` - Filter alerts by symbol

**Alert Model**:
```json
{
  "id": "guid",
  "symbol": "AAPL",
  "targetPrice": 180.00,
  "condition": "above",
  "createdAt": "2025-01-12T10:30:00Z"
}
```

## 🛠️ Technologies Used

- **.NET 8** - Web API framework
- **Docker** - Containerization
- **Azure Container Registry (ACR)** - Container image storage
- **Azure Kubernetes Service (AKS)** - Container orchestration
- **Kubernetes** - Deployments, Services, ConfigMaps
- **Azure CLI** - Infrastructure management

## 📁 Project Structure

```
MarketDataAggregator/
├── src/
│   ├── GatewayApi/
│   │   ├── Controllers/
│   │   ├── Services/
│   │   │   ├── PriceServiceClient.cs
│   │   │   └── AlertServiceClient.cs
│   │   ├── Program.cs
│   │   ├── Dockerfile
│   │   └── GatewayApi.csproj
│   ├── PriceService/
│   │   ├── Controllers/
│   │   │   └── PricesController.cs
│   │   ├── Program.cs
│   │   ├── Dockerfile
│   │   └── PriceService.csproj
│   └── AlertService/
│       ├── Controllers/
│       │   └── AlertsController.cs
│       ├── Models/
│       │   └── Alert.cs
│       ├── Program.cs
│       ├── Dockerfile
│       └── AlertService.csproj
├── k8s/
│   ├── gateway-deployment.yaml
│   ├── gateway-service.yaml
│   ├── price-deployment.yaml
│   ├── price-service.yaml
│   ├── alert-deployment.yaml
│   ├── alert-service.yaml
│   ├── configmap.yaml
│   └── ingress.yaml (optional)
├── postman/
│   └── market-data-api.postman_collection.json
└── README.md
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
- [kubectl](https://kubernetes.io/docs/tasks/tools/)
- Azure subscription

### Local Development

1. **Clone the repository**
```bash
git clone https://github.com/gastoncisneros/MarketDataAggregatorAPI.git
cd MarketDataAggregatorAPI
```

2. **Run services locally**
```bash
# Gateway API
cd src/GatewayApi
dotnet run

# Price Service
cd src/PriceService
dotnet run

# Alert Service
cd src/AlertService
dotnet run
```

3. **Test endpoints**
```bash
# Price Service
curl http://localhost:5081/api/prices/AAPL

# Alert Service
curl -X POST http://localhost:5082/api/alerts \
  -H "Content-Type: application/json" \
  -d '{"symbol":"AAPL","targetPrice":180,"condition":"above"}'

# Gateway API
curl http://localhost:5080/api/market/summary
```

## 🐳 Docker Build & Run

### Build Docker Images

```bash
# From project root
cd src/GatewayApi
docker build -t gatewayapi:v1 .

cd ../PriceService
docker build -t priceservice:v1 .

cd ../AlertService
docker build -t alertservice:v1 .
```

### Run with Docker

```bash
# Create network
docker network create market-network

# Run services
docker run -d --name price-service --network market-network -p 8081:8081 priceservice:v1
docker run -d --name alert-service --network market-network -p 8082:8082 alertservice:v1
docker run -d --name gateway-api --network market-network -p 8080:8080 gatewayapi:v1
```

## ☁️ Azure Deployment

### 1. Create Azure Resources

```bash
# Login to Azure
az login

# Create resource group
az group create --name rg-market-data --location eastus

# Create Azure Container Registry
az acr create --resource-group rg-market-data \
  --name gastonacr \
  --sku Basic

# Login to ACR
az acr login --name gastonacr
```

### 2. Push Images to ACR

```bash
# Tag images
docker tag gatewayapi:v1 gastonacr.azurecr.io/gatewayapi:v1
docker tag priceservice:v1 gastonacr.azurecr.io/priceservice:v1
docker tag alertservice:v1 gastonacr.azurecr.io/alertservice:v1

# Push to ACR
docker push gastonacr.azurecr.io/gatewayapi:v1
docker push gastonacr.azurecr.io/priceservice:v1
docker push gastonacr.azurecr.io/alertservice:v1
```

### 3. Create AKS Cluster

```bash
# Create AKS cluster (takes 5-10 minutes)
az aks create \
  --resource-group rg-market-data \
  --name aks-market-data \
  --node-count 2 \
  --enable-managed-identity \
  --attach-acr gastonacr \
  --generate-ssh-keys

# Get credentials
az aks get-credentials --resource-group rg-market-data --name aks-market-data

# Verify connection
kubectl get nodes
```

### 4. Deploy to Kubernetes

```bash
# Apply all configurations
kubectl apply -f k8s/

# Check deployment status
kubectl get deployments
kubectl get pods
kubectl get services

# Wait for external IP (LoadBalancer)
kubectl get service gateway-service -w
```

### 5. Test Deployed API

```bash
# Get external IP
EXTERNAL_IP=$(kubectl get service gateway-service -o jsonpath='{.status.loadBalancer.ingress[0].ip}')

# Test endpoints
curl http://$EXTERNAL_IP/api/health
curl http://$EXTERNAL_IP/api/market/summary
```

## 📊 Kubernetes Resources

### Deployments
- **gateway-deployment**: 2 replicas, LoadBalancer service
- **price-deployment**: 2 replicas, ClusterIP service
- **alert-deployment**: 2 replicas, ClusterIP service

### Resource Limits
```yaml
resources:
  requests:
    memory: "128Mi"
    cpu: "100m"
  limits:
    memory: "256Mi"
    cpu: "200m"
```

### Health Checks
All services include:
- Liveness probes (HTTP `/api/health`)
- Readiness probes (HTTP `/api/health`)
- Startup probes with 30s timeout

## 🧪 Testing

### Using Postman
Import the collection from `postman/market-data-api.postman_collection.json`

### Using curl

```bash
# Health check
curl http://<EXTERNAL_IP>/api/health

# Get price data
curl http://<EXTERNAL_IP>/api/market/summary

# Create alert
curl -X POST http://<EXTERNAL_IP>/api/alerts \
  -H "Content-Type: application/json" \
  -d '{
    "symbol": "AAPL",
    "targetPrice": 180.0,
    "condition": "above"
  }'

# Get alerts
curl http://<EXTERNAL_IP>/api/alerts/AAPL
```

## 📈 Monitoring & Debugging

### View Logs
```bash
# Gateway logs
kubectl logs -l app=gateway --tail=50 -f

# Price Service logs
kubectl logs -l app=price-service --tail=50 -f

# Alert Service logs
kubectl logs -l app=alert-service --tail=50 -f
```

### Debugging Pods
```bash
# Describe pod
kubectl describe pod <pod-name>

# Execute commands inside pod
kubectl exec -it <pod-name> -- /bin/bash

# Port forward for local testing
kubectl port-forward service/gateway-service 8080:80
```

### View All Resources
```bash
kubectl get all
kubectl get configmaps
kubectl get ingress
```

## 🔄 Rolling Updates

```bash
# Update image version
kubectl set image deployment/gateway-deployment \
  gateway=gastonacr.azurecr.io/gatewayapi:v2

# Watch rollout status
kubectl rollout status deployment/gateway-deployment

# Rollback if needed
kubectl rollout undo deployment/gateway-deployment
```

## 🧹 Cleanup

```bash
# Delete Kubernetes resources
kubectl delete -f k8s/

# Delete AKS cluster and all resources
az group delete --name rg-market-data --yes --no-wait
```

## 📚 Key Learnings

This project demonstrates:

✅ **Microservices Design**: Separation of concerns with independent services  
✅ **Containerization**: Docker best practices for .NET applications  
✅ **Kubernetes Orchestration**: Deployments, services, and service discovery  
✅ **Cloud Native**: Azure-native services (ACR, AKS)  
✅ **Service Communication**: Internal HTTP communication via Kubernetes DNS  
✅ **Scalability**: Horizontal scaling with replica sets  
✅ **Resilience**: Health checks, resource limits, rolling updates  
✅ **DevOps**: Infrastructure as code with YAML manifests

## 🎓 Interview Topics Covered

- **Microservices Architecture**: Gateway pattern, service boundaries
- **Kubernetes Concepts**: Pods, Deployments, Services, ConfigMaps, Ingress
- **Container Registry**: Image versioning, security scanning
- **Service Discovery**: ClusterIP vs LoadBalancer vs NodePort
- **High Availability**: Multiple replicas, health checks
- **Zero-Downtime Deployments**: Rolling updates strategy
- **Resource Management**: CPU/memory limits and requests
- **Cloud Native Patterns**: 12-factor app principles

## 👤 Author

**Gaston Cisneros**
- Senior .NET Backend Developer
- 8+ years of experience
- Specialization: Microservices, Azure, .NET Core

## 📝 License

This project is for educational and portfolio purposes.

---

*Built with ☕ and .NET*
