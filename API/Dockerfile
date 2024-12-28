# Use the official .NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

# Copy the published files from your local machine (outside the container)
FROM base AS final
WORKDIR /API

# Copy the published app from the host machine (local machine)
COPY ./publish .

# Set the entry point for your app
ENTRYPOINT ["dotnet", "API.dll"]
