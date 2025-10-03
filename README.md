# HR Management System

Clean Architecture .NET 8 API with CQRS, JWT authentication, and MySQL.

## 🏗️ Architecture
- **Clean Architecture** (API → Application → Domain → Infrastructure)
- **CQRS Pattern** with MediatR
- **Unit of Work** for transactions

## 🚀 Features
- User registration and listing
- JWT Authentication
- FluentValidation
- Centralized package management

## 🛠️ Tech Stack
- .NET 8, MySQL, EF Core
- MediatR, FluentValidation, JWT
- xUnit, Moq, Bogus, FluentAssertions

## 🧪 Testing
```bash
dotnet test
```

## 🔌 API Endpoints
- `POST v1/api/users` - Create user
- `GET v1/api/users` - List all users
- `GET v1/api/users/{id}` - Get user by ID

## 🔒 Security
- JWT Bearer authentication
- BCrypt password hashing
- Role-based access (Admin/Regular)
