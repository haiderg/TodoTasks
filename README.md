# TodoTasks API

A modern **ASP.NET Core 10** Web API demonstrating enterprise-level architecture patterns and best practices for task management.

## 🏗️ Architecture

This project implements **Clean Architecture** with clear separation of concerns:

```
src/
├── TodoTasks.API/          # Web API Layer (Controllers, Program.cs)
├── TodoTasks.Application/  # Business Logic Layer (Services, Interfaces)
├── TodoTasks.Domain/       # Domain Layer (Entities, Value Objects, Repositories)
└── TodoTasks.Infrastructure/ # Data Access Layer (EF Core, Repositories)
```

## 🚀 Key Features

- **Clean Architecture** with dependency inversion
- **Domain-Driven Design** principles
- **JWT Authentication** for secure API access
- **Entity Framework Core 10** with SQL Server
- **Repository Pattern** for data access abstraction
- **Rich Domain Models** with business logic encapsulation
- **Value Objects** for type-safe operations
- **Swagger/OpenAPI** documentation with Bearer token support
- **Unit Tests** for Application and Domain layers
- **Dependency Injection** throughout all layers

## 🛠️ Technologies

- **.NET 10** - Latest framework version
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core 10** - ORM for data access
- **SQL Server** - Database provider
- **JWT Bearer Authentication** - Secure token-based authentication
- **Swashbuckle.AspNetCore 6.9** - API documentation with Bearer support
- **xUnit** - Unit testing framework
- **C# 13** - Latest language features

## 📋 Domain Model

### TodoTask Entity
- Rich domain model with business rules
- Encapsulated state changes through methods
- Validation at domain level
- Support for categories, due dates, and assignments

### Category Entity
- Color-coded task categorization
- Validation for name length and format
- Update tracking with timestamps

## 🔧 Technical Highlights

### Clean Architecture Implementation
- **Domain Layer**: Pure business logic, no external dependencies
- **Application Layer**: Use cases and business workflows
- **Infrastructure Layer**: External concerns (database, external APIs)
- **API Layer**: HTTP concerns and request/response handling

### Domain-Driven Design
- **Entities** with identity and lifecycle
- **Value Objects** for immutable data structures
- **Repository Interfaces** in domain layer
- **Domain Services** for complex business operations

### Entity Framework Core Features
- **Code-First** approach with migrations
- **Seed Data** for initial categories and tasks
- **Relationship Configuration** between entities
- **Repository Pattern** implementation

## 🚦 Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2024 or VS Code

### Setup
1. Clone the repository
2. Update connection string in `appsettings.json`
3. Run database migrations:
   ```bash
   dotnet ef database update --project src/TodoTasks.API
   ```
4. Start the application:
   ```bash
   dotnet run --project src/TodoTasks.API
   ```

### API Documentation
Navigate to `/swagger` when running in development mode to explore the API endpoints.

**Testing Protected Endpoints:**
1. Use `/api/auth/login` to get a JWT token
2. Click the **Authorize** button in Swagger UI
3. Enter your JWT token
4. Test protected endpoints

## 📊 Sample Data

The application includes seed data with:
- **3 Categories**: Work, Personal, Shopping (with color coding)
- **3 Sample Tasks**: Demonstrating different states and categories

## 🎯 API Endpoints

### Authentication
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register new user |
| POST | `/api/auth/login` | Login and get JWT token |

### Tasks (Protected)
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/todotasks` | Get all tasks |
| GET | `/api/todotasks/{id}` | Get task by ID |
| POST | `/api/todotasks` | Create new task |
| PUT | `/api/todotasks/{id}` | Update existing task |
| DELETE | `/api/todotasks/{id}` | Delete task |
| POST | `/api/todotasks/{id}/complete` | Mark task as complete |

### Categories (Protected)
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/categories` | Get all categories |
| GET | `/api/categories/{id}` | Get category by ID |
| POST | `/api/categories` | Create new category |
| PUT | `/api/categories/{id}` | Update existing category |
| DELETE | `/api/categories/{id}` | Delete category |

## 🏆 Skills Demonstrated

### ASP.NET Core Expertise
- ✅ **Web API Development** - RESTful API design
- ✅ **JWT Authentication** - Token-based security
- ✅ **Dependency Injection** - Built-in DI container usage
- ✅ **Configuration Management** - appsettings.json handling
- ✅ **Middleware Pipeline** - HTTP request processing
- ✅ **Model Binding & Validation** - Request/response handling
- ✅ **Swagger Integration** - API documentation with Bearer auth

### Architecture & Design Patterns
- ✅ **Clean Architecture** - Layered application design
- ✅ **Repository Pattern** - Data access abstraction
- ✅ **Dependency Inversion** - Interface-based programming
- ✅ **Domain-Driven Design** - Rich domain models
- ✅ **SOLID Principles** - Clean, maintainable code

### Entity Framework Core
- ✅ **Code-First Migrations** - Database schema management
- ✅ **DbContext Configuration** - Database context setup
- ✅ **Relationship Mapping** - Entity relationships
- ✅ **Seed Data** - Initial data population
- ✅ **Async Operations** - Non-blocking database operations

### Testing
- ✅ **Unit Tests** - Application layer service tests
- ✅ **Domain Tests** - Domain entity and value object tests
- ✅ **xUnit Framework** - Modern testing practices
- ✅ **Test Isolation** - Independent test execution

### Modern C# Features
- ✅ **Nullable Reference Types** - Null safety
- ✅ **Record Types** - Immutable data structures
- ✅ **Pattern Matching** - Modern C# syntax
- ✅ **Primary Constructors** - Concise constructor syntax
- ✅ **Global Using Statements** - Reduced boilerplate

## 📈 Implemented Features

- ✅ **Authentication & Authorization (JWT)** - Secure token-based auth
- ✅ **Unit Tests** - Application and Domain layer coverage
- [ ] CQRS with MediatR
- [ ] Integration Tests
- [ ] Docker containerization
- [ ] CI/CD pipeline
- [ ] Caching with Redis
- [ ] Logging with Serilog
- [ ] Health checks
- [ ] Rate limiting
- [ ] API versioning

---

*This project demonstrates production-ready ASP.NET Core development practices suitable for enterprise applications.*