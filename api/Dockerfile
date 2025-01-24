# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and project files
COPY ./api/api.sln ./api/
COPY ./api/*.csproj ./api/

# Restore dependencies
WORKDIR /app/api
RUN dotnet restore

# Clean the build directories
RUN dotnet clean

# Copy the rest of the application files
COPY ./api/ ./api/

# Ensure permissions for bin and obj directories
# RUN chmod -R 777 /app/api/bin /app/api/obj

# Publish the application to /app/publish
RUN dotnet publish -c Release -o /app/publish --no-restore

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/publish .

# Expose the port your app runs on
EXPOSE 5000

# Run the application
ENTRYPOINT ["dotnet", "api.dll"]
