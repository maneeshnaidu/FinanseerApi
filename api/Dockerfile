# Use official .NET 8 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project and restore dependencies
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /publish

# Use a smaller runtime image for production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /publish ./

# Expose the API port
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "FinanseerApi.dll"]
