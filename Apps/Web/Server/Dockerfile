#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Apps/Web/Server/WebApp.Server.csproj", "Apps/Web/Server/"]
COPY ["Apps/Web/Shared/WebApp.Shared.csproj", "Apps/Web/Shared/"]
COPY ["Apps/Web/Client/WebApp.Client.csproj", "Apps/Web/Client/"]
RUN dotnet restore "Apps/Web/Server/WebApp.Server.csproj"
COPY . .
WORKDIR "/src/Apps/Web/Server"
RUN dotnet build "WebApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.Server.dll"]