#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

RUN apt-get update && apt-get install -y nano


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApiSignosVitales/WebApiSignosVitales.csproj", "WebApiSignosVitales/"]
COPY ["DataSignosVitales/DataSignosVitales.csproj", "DataSignosVitales/"]
COPY ["LogicaSignosVitales/LogicaSignosVitales.csproj", "LogicaSignosVitales/"]
RUN dotnet restore "./WebApiSignosVitales/./WebApiSignosVitales.csproj"
COPY . .
WORKDIR "/src/WebApiSignosVitales"
RUN dotnet build "./WebApiSignosVitales.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApiSignosVitales.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "WebApiSignosVitales.dll"]