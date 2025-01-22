# Use the official .NET 8 SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /FinanseerApi

# Copy the solution file and project files
COPY ./api/api.sln ./api/
COPY ./api/*/*.csproj ./src/

# Restore dependencies
WORKDIR /FinanseerApi
RUN dotnet restore ../api/api.sln

# Copy the entire project content
WORKDIR /FinanseerApi
COPY ./api/ ./api/

# Build and publish the app
WORKDIR /FinanseerApi
RUN dotnet publish ../api/api.sln -c Release -o /app/publish

# Use a runtime image for the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /FinanseerApi
COPY --from=build /app/publish .

# Expose the default ASP.NET Core port
EXPOSE 5000

# Run the application
ENTRYPOINT ["dotnet", "api.dll"]
