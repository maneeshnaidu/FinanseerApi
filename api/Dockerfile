# Use official .NET 8 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution and project file first
COPY api/api.sln ./
COPY api/api.csproj ./api/

# Restore dependencies
WORKDIR /app/api
RUN dotnet restore

# Copy everything else
COPY api/. ./

# Build and publish
RUN dotnet build --no-restore
RUN dotnet publish -c Release -o /publish

# Use a smaller runtime image for production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /publish ./

# Expose the API port
EXPOSE 8080
ENTRYPOINT ["dotnet", "api.dll"]

