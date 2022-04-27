#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5014
EXPOSE 5015

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/UI/UI.API/UI.API.csproj", "Services/UI/UI.API/"]
COPY ["Services/UI/UI.Infrastructure/UI.Infrastructure.csproj", "Services/UI/UI.Infrastructure/"]
COPY ["Services/UI/UI.Application/UI.Application.csproj", "Services/UI/UI.Application/"]
COPY ["Services/UI/UI.Domain/UI.Domain.csproj", "Services/UI/UI.Domain/"]
COPY ["Services/Services.Shared/Services.Shared.csproj", "Services/Services.Shared/"]
RUN dotnet restore "Services/UI/UI.API/UI.API.csproj"
COPY . .
WORKDIR "/src/Services/UI/UI.API"
RUN dotnet build "UI.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UI.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UI.API.dll"]