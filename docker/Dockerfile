# Use the official .NET Core SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory in the container
WORKDIR /app

# Copy the project file and restore dependencies
COPY InputService.csproj ./
RUN dotnet restore

# Copy the remaining source code
COPY . ./

# Build the application
RUN dotnet publish InputService.csproj -c Release -o out

# Create a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory to /app
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build-env /app/out .

# Expose the port on which the application will run
EXPOSE 8080
EXPOSE 433

# Define the entry point for the container
ENTRYPOINT ["dotnet", "InputService.dll"]