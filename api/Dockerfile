# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution and project files
COPY ./api/api.sln ./api/
COPY ./api/*.csproj ./api/

# Restore dependencies
WORKDIR /app/api
RUN dotnet restore

# Copy the rest of the app files
COPY ./api/ ./api/

# Publish the application to /app/publish
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/publish .

# Expose the port your app runs on
EXPOSE 5000

# Run the application
ENTRYPOINT ["dotnet", "api.dll"]
