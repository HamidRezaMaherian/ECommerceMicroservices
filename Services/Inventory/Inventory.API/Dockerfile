#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5006
EXPOSE 5007

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Inventory/Inventory.API/Inventory.API.csproj", "Services/Inventory/Inventory.API/"]
COPY ["Services/Inventory/Inventory.Infrastructure/Inventory.Infrastructure.csproj", "Services/Inventory/Inventory.Infrastructure/"]
COPY ["Services/Inventory/Inventory.Application/Inventory.Application.csproj", "Services/Inventory/Inventory.Application/"]
COPY ["Services/Inventory/Inventory.Domain/Inventory.Domain.csproj", "Services/Inventory/Inventory.Domain/"]
COPY ["Services/Services.Shared/Services.Shared.csproj", "Services/Services.Shared/"]
RUN dotnet restore "Services/Inventory/Inventory.API/Inventory.API.csproj"
COPY . .
WORKDIR "/src/Services/Inventory/Inventory.API"
RUN dotnet build "Inventory.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Inventory.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory.API.dll"]