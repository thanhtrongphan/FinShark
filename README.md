# API for Portfolio Management

## Overview
This project is a .NET Core Web API designed to manage user portfolios, including stocks and comments. The API provides endpoints for user authentication, portfolio management, and stock information retrieval.

## Project Structure
The project is organized into several key folders and files:

- **Controllers**: Contains the API controllers that handle HTTP requests.
  - `AccountController.cs`
  - `CommentController.cs`
  - `PortfolioController.cs`
  - `StockController.cs`

- **Data**: Contains the database context and migration files.
  - `ApplicationDBContext.cs`: Manages interactions between the application and the database.

- **Dtos**: Contains Data Transfer Objects for various entities.
  - `Account/`
  - `Comment/`
  - `Stock/`

- **Extensions**: Contains extension methods and helper classes.

- **Helpers**: Contains helper classes for various functionalities.

- **Interfaces**: Contains interface definitions for repositories and services.
  - `IPortfolioRepository.cs`

- **Mappers**: Contains mapping configurations for entities.

- **Migrations**: Contains Entity Framework Core migration files.

- **Models**: Contains the entity models representing the database tables.

- **Repository**: Contains repository implementations for data access.
  - `PortfolioRepository.cs`

- **Service**: Contains service implementations for business logic.

- **Program.cs**: Configures the application, including services, middleware, and routing.

## Key Features
- **User Authentication**: Uses ASP.NET Core Identity for user authentication and authorization.
- **Portfolio Management**: Allows users to add, delete, and retrieve stocks in their portfolio.
- **Stock Information**: Integrates with external services to fetch stock information.
- **Database Integration**: Uses Entity Framework Core for database interactions.

## How to Run
1. **appsettings.json**:
  Setting connect database in DefaultConnection
2. **Database Migrations**:
    Add a migration:
   ```sh
   dotnet ef migrations add init
3. **Apply migrations**:
  ```sh
  dotnet ef database update
  ```
4. **Run the Project**:
  ```sh
   dotnet watch run
  ```
## How to Run
- .NET Core 8.0
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server
- Swagger for API documentation
