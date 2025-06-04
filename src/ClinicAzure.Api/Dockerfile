FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClinicAzure.Api/ClinicAzure.Api.csproj", "ClinicAzure.Api/"]
COPY ["ClinicAzure.Domain/ClinicAzure.Domain.csproj", "ClinicAzure.Domain/"]
COPY ["ClinicAzure.Shared/ClinicAzure.Shared.csproj", "ClinicAzure.Shared/"]
COPY ["ClinicAzure.Infrastructure/ClinicAzure.Infrastructure.csproj", "ClinicAzure.Infrastructure/"]
COPY ["ClinicAzure.Application/ClinicAzure.Application.csproj", "ClinicAzure.Application/"]

RUN dotnet restore "./ClinicAzure.Api/ClinicAzure.Api.csproj"
COPY . .
WORKDIR "/src/ClinicAzure.Api"
RUN dotnet build "./ClinicAzure.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ClinicAzure.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClinicAzure.Api.dll"]