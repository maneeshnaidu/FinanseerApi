# Use the official .NET 8 SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file and restore dependencies
COPY FinanseerApi/api/api.sln .
COPY FinanseerApi/api/*/*.csproj ./src/
WORKDIR /app/src
RUN dotnet restore ../api.sln

# Copy all source files
WORKDIR /app
COPY FinanseerApi/api/ .

# Build and publish the application
WORKDIR /app/src
RUN dotnet publish ../api.sln -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose the default ASP.NET Core port
EXPOSE 5000

# Run the application
ENTRYPOINT ["dotnet", "api.dll"]
