# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files (for restore caching)
COPY JewelryBox.sln ./
COPY JewelryBox.API/JewelryBox.API.csproj JewelryBox.API/
COPY JewelryBox.Application/JewelryBox.Application.csproj JewelryBox.Application/
COPY JewelryBox.Core/JewelryBox.Core.csproj JewelryBox.Core/
COPY JewelryBox.Domain/JewelryBox.Domain.csproj JewelryBox.Domain/
COPY JewelryBox.Infrastructure/JewelryBox.Infrastructure.csproj JewelryBox.Infrastructure/

RUN dotnet restore

# Copy all source and build
COPY . .
WORKDIR /src/JewelryBox.API
RUN dotnet publish -c Release -o /app/publish

# ---------- Runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port (informational)
EXPOSE 80

# Start app, bind to Render's PORT or fallback to 80
ENTRYPOINT ["sh", "-c", "dotnet JewelryBox.API.dll --urls http://0.0.0.0:${PORT:-80}"]
