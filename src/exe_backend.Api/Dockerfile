# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/exe_backend.Api/exe_backend.Api.csproj", "src/exe_backend.Api/"]
COPY ["src/exe_backend.Application/exe_backend.Application.csproj", "src/exe_backend.Application/"]
COPY ["src/exe_backend.Contract/exe_backend.Contract.csproj", "src/exe_backend.Contract/"]
COPY ["src/exe_backend.Domain/exe_backend.Domain.csproj", "src/exe_backend.Domain/"]
COPY ["src/exe_backend.Infrastructure/exe_backend.Infrastructure.csproj", "src/exe_backend.Infrastructure/"]
COPY ["src/exe_backend.Persistence/exe_backend.Persistence.csproj", "src/exe_backend.Persistence/"]
COPY ["src/exe_backend.Presentation/exe_backend.Presentation.csproj", "src/exe_backend.Presentation/"]
RUN dotnet restore "./src/exe_backend.Api/exe_backend.Api.csproj"
COPY . .
WORKDIR "/src/src/exe_backend.Api"
RUN dotnet build "./exe_backend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./exe_backend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "exe_backend.Api.dll"]