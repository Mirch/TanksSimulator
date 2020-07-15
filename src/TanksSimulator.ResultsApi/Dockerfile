FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./TanksSimulator.Shared/*.csproj ./
COPY ./TanksSimulator.ResultsApi/*.csproj ./
RUN dotnet restore TanksSimulator.ResultsApi.csproj

# Copy everything else and build
COPY ./TanksSimulator.Shared ./
COPY ./TanksSimulator.ResultsApi ./
RUN dotnet publish TanksSimulator.ResultsApi.csproj -c Release -o out --no-cache

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TanksSimulator.ResultsApi.dll"]
