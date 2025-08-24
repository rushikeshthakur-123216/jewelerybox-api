# JewelryBox API - Phase 1

A professional .NET Core API for managing jewelry collections with authentication using JWT tokens and PostgreSQL database.

## Architecture

This project follows Clean Architecture principles with the following layers:

- **JewelryBox.API**: Presentation layer with controllers
- **JewelryBox.Application**: Application services and DTOs
- **JewelryBox.Domain**: Domain entities and interfaces
- **JewelryBox.Infrastructure**: Data access and external services
- **JewelryBox.Core**: Shared utilities and extensions

## Features (Phase 1)

- ✅ User Registration
- ✅ User Login with JWT Authentication
- ✅ Password hashing with BCrypt
- ✅ XML-based SQL queries (no stored procedures)
- ✅ PostgreSQL database support
- ✅ Clean architecture with dependency injection
- ✅ JWT token validation

## Prerequisites

- .NET 8.0 SDK
- PostgreSQL database
- Your favorite IDE (Visual Studio, VS Code, etc.)

## Setup Instructions

### 1. Database Setup

1. Create a PostgreSQL database named `jewelrybox`
2. Run the SQL script in `Database/CreateTables.sql` to create the required tables

### 2. Configuration

1. Update the `appsettings.json` file with your database connection string:
```json
{
  "DatabaseSettings": {
    "ConnectionString": "Host=localhost;Database=jewelrybox;Username=your_username;Password=your_password",
    "Provider": "PostgreSQL"
  }
}
```

2. Update the JWT secret key in `appsettings.json`:
```json
{
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-with-at-least-32-characters",
    "Issuer": "JewelryBox",
    "Audience": "JewelryBoxUsers",
    "ExpirationMinutes": 60
  }
}
```

### 3. Build and Run

```bash
# Restore packages
dotnet restore

# Build the solution
dotnet build

# Run the API
dotnet run --project JewelryBox.API
```

The API will be available at `https://localhost:7001` (or the port shown in your terminal).

## API Endpoints

### Authentication

#### Register User
```
POST /api/auth/register
Content-Type: application/json

{
  "username": "johndoe",
  "email": "john@example.com",
  "password": "password123",
  "confirmPassword": "password123",
  "firstName": "John",
  "lastName": "Doe"
}
```

#### Login
```
POST /api/auth/login
Content-Type: application/json

{
  "username": "johndoe",
  "password": "password123"
}
```

#### Validate Token
```
POST /api/auth/validate
Content-Type: application/json

"your-jwt-token-here"
```

## Testing with Swagger

1. Navigate to `https://localhost:7001/swagger` in your browser
2. Use the Swagger UI to test the authentication endpoints
3. Copy the JWT token from the login response for testing protected endpoints

## Testing with Postman

### Register a new user:
1. Set method to `POST`
2. URL: `https://localhost:7001/api/auth/register`
3. Headers: `Content-Type: application/json`
4. Body (raw JSON):
```json
{
  "username": "testuser",
  "email": "test@example.com",
  "password": "password123",
  "confirmPassword": "password123",
  "firstName": "Test",
  "lastName": "User"
}
```

### Login:
1. Set method to `POST`
2. URL: `https://localhost:7001/api/auth/login`
3. Headers: `Content-Type: application/json`
4. Body (raw JSON):
```json
{
  "username": "testuser",
  "password": "password123"
}
```

## Project Structure

```
JewelryBox/
├── JewelryBox.API/                 # Web API project
│   ├── Controllers/                # API controllers
│   ├── Middleware/                 # Custom middleware
│   └── appsettings.json           # Configuration
├── JewelryBox.Application/         # Application layer
│   ├── DTOs/                      # Data transfer objects
│   ├── Interfaces/                 # Application interfaces
│   └── Services/                   # Application services
├── JewelryBox.Domain/              # Domain layer
│   ├── Entities/                   # Domain entities
│   ├── Interfaces/                 # Repository interfaces
│   └── Enums/                      # Domain enums
├── JewelryBox.Infrastructure/      # Infrastructure layer
│   ├── Data/                       # Database connection
│   ├── Repositories/               # Repository implementations
│   ├── Services/                   # Infrastructure services
│   └── Queries/                    # XML query files
├── JewelryBox.Core/                # Shared utilities
│   ├── Configuration/              # Configuration classes
│   └── Extensions/                 # Service extensions
└── Database/                       # Database scripts
    └── CreateTables.sql           # Database schema
```

## Key Features

### XML Query Management
- SQL queries are stored in XML files instead of stored procedures
- Easy to maintain and version control
- Supports multiple database providers

### Database Provider Flexibility
- Currently configured for PostgreSQL
- Easy to switch to Oracle, SQL Server, etc. by changing the connection factory
- Provider-specific queries can be added to XML files

### Security
- JWT token-based authentication
- BCrypt password hashing
- Input validation and sanitization
- CORS configuration

## Next Phases

- Phase 2: Jewelry Box CRUD operations
- Phase 3: Jewelry Items management
- Phase 4: Advanced features (search, filters, etc.)

## Troubleshooting

### Common Issues

1. **Database Connection Error**: Ensure PostgreSQL is running and connection string is correct
2. **JWT Token Issues**: Verify the secret key is at least 32 characters long
3. **XML Query Loading**: Ensure the XML files are copied to the output directory

### Logs
Check the console output for detailed error messages and stack traces.

## Contributing

1. Follow the existing code structure and naming conventions
2. Add appropriate unit tests for new features
3. Update documentation for any API changes
4. Ensure all tests pass before submitting changes
