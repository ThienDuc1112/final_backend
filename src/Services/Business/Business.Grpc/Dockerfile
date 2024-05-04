#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Business/Business.Grpc/Business.Grpc.csproj", "Services/Business/Business.Grpc/"]
COPY ["Services/Business/Business.Application/Business.Application.csproj", "Services/Business/Business.Application/"]
COPY ["Services/Business/Business.Domain/Business.Domain.csproj", "Services/Business/Business.Domain/"]
COPY ["Services/Business/Business.Infrastructure/Business.Infrastructure.csproj", "Services/Business/Business.Infrastructure/"]
RUN dotnet restore "Services/Business/Business.Grpc/Business.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Business/Business.Grpc"
RUN dotnet build "Business.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Business.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Business.Grpc.dll"]