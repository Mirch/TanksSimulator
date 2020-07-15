FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./TanksSimulator.Shared/*.csproj ./
COPY ./TanksSimulator.Game/*.csproj ./
COPY ./TanksSimulator.WebApi/*.csproj ./
RUN dotnet restore TanksSimulator.WebApi.csproj

# Copy everything else and build
COPY ./TanksSimulator.Shared ./
COPY ./TanksSimulator.Game ./
COPY ./TanksSimulator.WebApi ./
RUN dotnet publish TanksSimulator.WebApi.csproj -c Release -o out --no-cache

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TanksSimulator.WebApi.dll"]
