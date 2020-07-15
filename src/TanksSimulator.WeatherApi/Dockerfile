FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./TanksSimulator.WeatherApi/*.csproj ./
RUN dotnet restore TanksSimulator.WeatherApi.csproj

# Copy everything else and build
COPY ./TanksSimulator.WeatherApi ./
RUN dotnet publish TanksSimulator.WeatherApi.csproj -c Release -o out --no-cache

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TanksSimulator.WeatherApi.dll"]
