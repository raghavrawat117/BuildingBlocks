Your launchSettings.json shows that the application runs on:

HTTP: 5266
HTTPS: 7020 (development only)

Inside Docker, it's usually best to expose only the HTTP port and let Kubernetes, Nginx, an Ingress Controller, API Gateway, or a load balancer handle HTTPS.

Corrected Dockerfile
# 1. Base Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

WORKDIR /app

# Configure Kestrel to listen on port 5266
ENV ASPNETCORE_URLS=http://+:5266

EXPOSE 5266

# 2. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["LearnersApi.csproj", "./"]

RUN dotnet restore "LearnersApi.csproj"

COPY . .

RUN dotnet build "LearnersApi.csproj" \
    -c Release \
    -o /app/build

# 3. Publish Stage
FROM build AS publish

RUN dotnet publish "LearnersApi.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# 4. Final Runtime Stage
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LearnersApi.dll"]

Build
docker build -t learners-api .

Run
docker run -d -p 5266:5266 --name learners-api learners-api

Test
curl http://localhost:5266


or open:

http://localhost:5266/swagger


(if Swagger is enabled)

One More Thing

Your Dockerfile assumes LearnersApi.csproj is in the same folder as the Dockerfile. If your structure is:

Solution
│
├── LearnersApi.sln
│
└── LearnersApi
    ├── LearnersApi.csproj
    ├── Program.cs
    └── ...


then the COPY commands need to be changed to:

COPY ["LearnersApi/LearnersApi.csproj", "LearnersApi/"]
RUN dotnet restore "LearnersApi/LearnersApi.csproj"

COPY . .
WORKDIR /src/LearnersApi


Your posted Dockerfile suggests the .csproj and Dockerfile are in the same directory, but verify the folder structure because that's one of the most common reasons Docker builds fail.