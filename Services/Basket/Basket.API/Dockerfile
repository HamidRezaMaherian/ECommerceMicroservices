#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Basket/Basket.API/Basket.API.csproj", "Services/Basket/Basket.API/"]
COPY ["Services/Basket/Basket.Infrastructure/Basket.Infrastructure.csproj", "Services/Basket/Basket.Infrastructure/"]
COPY ["Services/Basket/Basket.Application/Basket.Application.csproj", "Services/Basket/Basket.Application/"]
COPY ["Services/Basket/Basket.Domain/Basket.Domain.csproj", "Services/Basket/Basket.Domain/"]
COPY ["Services/Services.Shared/Services.Shared.csproj", "Services/Services.Shared/"]
RUN dotnet restore "Services/Basket/Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "/src/Services/Basket/Basket.API"
RUN dotnet build "Basket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]