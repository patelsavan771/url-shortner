# Use the official .NET 8 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use a lightweight runtime image for final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

ENV MYSQL_ROOT_PASSWORD=password
ENV MYSQL_DATABASE=urlshortener
ENV MYSQL_USER=user
ENV MYSQL_PASSWORD=userpassword
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ALLOWED_ORIGINS=http://127.0.0.1:8081,http://127.0.0.1:5500,https://savan.site,http://127.0.0.1:8081,http://localhost:8081/
ENV USER_FACING_SERVER=http://localhost:5149

# Copy build output from previous stage
COPY --from=build /app/out .

# Expose port (adjust as needed)
EXPOSE 5149

# Set the entry point to the app
ENTRYPOINT ["dotnet", "url-shortner-api.dll"]
