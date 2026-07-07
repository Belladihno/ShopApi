# ShopifyApi

A .NET 8 Web API for managing products with JWT authentication — built as a C# course project.

## Tech Stack

- **.NET 8** — ASP.NET Core Web API
- **Entity Framework Core** — ORM for SQL Server (LocalDB)
- **SQL Server (LocalDB)** — database
- **JWT Bearer Auth** — token-based authentication
- **BCrypt** — password hashing
- **FluentValidation** — request validation
- **Swagger** — API docs UI

## Features

- **Products CRUD** — Create, Read, Update, Delete products
- **Auth** — Register and Login with JWT tokens
- **Validation** — input validation via FluentValidation
- **Global error handling** — catches unhandled exceptions and returns clean JSON errors

## Project Structure

```
ShopifyApi/
├── Common/              # Shared models (ApiErrorResponse)
├── Controllers/         # API endpoints (ProductsController)
├── Data/                # EF Core DbContext
├── DTOs/               # Request/Response data transfer objects
│   ├── Auth/
│   └── Products/
├── Interfaces/          # Service contracts (IProductService, IAuthService, ITokenService)
├── Mappings/            # Model <-> DTO mapping extension methods
├── Middleware/          # Global exception handler
├── Migrations/          # EF Core database migrations
├── Models/              # Domain models (Product, User)
├── Properties/          # Launch settings
├── Services/            # Business logic (ProductService, AuthService, TokenService)
├── Validators/          # FluentValidation validators
├── Program.cs           # App entry point & service registration
└── appsettings.json     # Configuration (connection string, JWT settings)
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server LocalDB (comes with Visual Studio)

### Setup

```bash
# Clone the repo
git clone https://github.com/Belladihno/ShopApi.git
cd ShopifyApi

# Update database (creates LocalDB and runs migrations)
dotnet ef database update

# Run the API
dotnet run
```

The API will be available at `http://localhost:5054` with Swagger UI at `http://localhost:5054/swagger`.

## API Endpoints

### Products (`/api/products`)

| Method | Endpoint           | Description        | Auth Required |
|--------|--------------------|--------------------|---------------|
| GET    | `/api/products`    | Get all products   | No            |
| GET    | `/api/products/{id}` | Get product by ID | No            |
| POST   | `/api/products`    | Create a product   | No            |
| PUT    | `/api/products/{id}` | Update a product | No            |
| DELETE | `/api/products/{id}` | Delete a product | No            |

### Auth (`/api/auth`)

| Method | Endpoint       | Description       | Auth Required |
|--------|----------------|-------------------|---------------|
| POST   | `/api/auth/register` | Register a new user | No         |
| POST   | `/api/auth/login`    | Login and get JWT  | No         |

## Configuration

Key settings in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ShopifyApiDb;Trusted_Connection=True;"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key",
    "Issuer": "ShopifyApi",
    "Audience": "ShopifyApiClients",
    "ExpiryMinutes": 60
  }
}
```
