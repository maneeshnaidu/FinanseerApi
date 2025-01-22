# Use the official .NET 8 SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file and project files
COPY ./api/api.sln ./api/
COPY ./api/*/*.csproj ./src/

# Restore dependencies
WORKDIR /app/src
RUN dotnet restore ../api/api.sln

# Copy the entire project content
WORKDIR /app
COPY ./api/ ./api/

# Build and publish the app
WORKDIR /app/src
RUN dotnet publish ../api/api.sln -c Release -o /app/publish

# Use a runtime image for the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose the default ASP.NET Core port
EXPOSE 5000

# Run the application
ENTRYPOINT ["dotnet", "api.dll"]
