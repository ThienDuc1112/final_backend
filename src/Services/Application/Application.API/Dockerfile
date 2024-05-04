#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Application/Application.API/Application.API.csproj", "Services/Application/Application.API/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
COPY ["Services/Application/Application.Infrastructure/Application.Infrastructure.csproj", "Services/Application/Application.Infrastructure/"]
COPY ["Services/Application/Application.Domain/Application.Domain.csproj", "Services/Application/Application.Domain/"]
RUN dotnet restore "Services/Application/Application.API/Application.API.csproj"
COPY . .
WORKDIR "/src/Services/Application/Application.API"
RUN dotnet build "Application.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Application.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Application.API.dll"]