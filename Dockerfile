# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Sales.API/Sales.API.csproj", "Sales.API/"]
COPY ["Sales.Application/Sales.Application.csproj", "Sales.Application/"]
COPY ["Sales.Domain/Sales.Domain.csproj", "Sales.Domain/"]
COPY ["Sales.Infrastructure/Sales.Infrastructure.csproj", "Sales.Infrastructure/"]
RUN dotnet restore "Sales.API/Sales.API.csproj"

COPY . .
WORKDIR "/src/Sales.API"
RUN dotnet publish "Sales.API.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Sales.API.dll"]
