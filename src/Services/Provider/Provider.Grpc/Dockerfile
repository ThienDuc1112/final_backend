#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Provider/Provider.Grpc/Provider.Grpc.csproj", "Services/Provider/Provider.Grpc/"]
COPY ["Services/Provider/Provider.Application/Provider.Application.csproj", "Services/Provider/Provider.Application/"]
COPY ["Services/Provider/Provider.Domain/Provider.Domain.csproj", "Services/Provider/Provider.Domain/"]
COPY ["Services/Provider/Provider.Infrastructure/Provider.Infrastructure.csproj", "Services/Provider/Provider.Infrastructure/"]
RUN dotnet restore "Services/Provider/Provider.Grpc/Provider.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Provider/Provider.Grpc"
RUN dotnet build "Provider.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Provider.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Provider.Grpc.dll"]