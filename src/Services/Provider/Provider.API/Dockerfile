#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Provider/Provider.API/Provider.API.csproj", "Services/Provider/Provider.API/"]
COPY ["Services/Provider/Provider.Application/Provider.Application.csproj", "Services/Provider/Provider.Application/"]
COPY ["Services/Provider/Provider.Domain/Provider.Domain.csproj", "Services/Provider/Provider.Domain/"]
COPY ["Services/Provider/Provider.Infrastructure/Provider.Infrastructure.csproj", "Services/Provider/Provider.Infrastructure/"]
RUN dotnet restore "Services/Provider/Provider.API/Provider.API.csproj"
COPY . .
WORKDIR "/src/Services/Provider/Provider.API"
RUN dotnet build "Provider.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Provider.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Provider.API.dll"]