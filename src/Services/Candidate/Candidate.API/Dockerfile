#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Candidate/Candidate.API/Candidate.API.csproj", "Services/Candidate/Candidate.API/"]
COPY ["Services/Candidate/Candidate.Application/Candidate.Application.csproj", "Services/Candidate/Candidate.Application/"]
COPY ["Services/Candidate/Candidate.Domain/Candidate.Domain.csproj", "Services/Candidate/Candidate.Domain/"]
COPY ["Services/Candidate/Candidate.Infrastructure/Candidate.Infrastructure.csproj", "Services/Candidate/Candidate.Infrastructure/"]
RUN dotnet restore "Services/Candidate/Candidate.API/Candidate.API.csproj"
COPY . .
WORKDIR "/src/Services/Candidate/Candidate.API"
RUN dotnet build "Candidate.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Candidate.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Candidate.API.dll"]