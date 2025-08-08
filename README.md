# Manager Zero User Service



![.NET Core](https://img.shields.io/badge/.NET-9.0-blue)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

---
Manager Zero User Service is an open-source, modular user management microservice built with .NET 9. It is designed to help developers quickly and easily integrate secure authentication, user and role management, and fine-grained permission controls into their applications. With a focus on developer experience, the project provides clear documentation, simple setup, and support for modern deployment environments like Docker and Kubernetes. Whether for SaaS, enterprise, or personal projects, this service accelerates development and ensures robust security and flexibility.


## Table of Contents
- [Manager Zero User Service](#manager-zero-user-service)
  - [Table of Contents](#table-of-contents)
  - [Features](#features)
    - [Core Functionality](#core-functionality)
    - [Advanced Features](#advanced-features)
  - [Architecture Overview](#architecture-overview)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
  - [Domain Model](#domain-model)
    - [Key Entities](#key-entities)
  - [Permission System](#permission-system)
    - [Example Permissions](#example-permissions)
    - [Usage Examples](#usage-examples)
    - [Testing](#testing)
  - [Deployment](#deployment)
    - [Docker](#docker)
    - [Kubernetes](#kubernetes)
  - [Contributing](#contributing)

## Features

### Core Functionality
- ✅ JWT Authentication with refresh tokens
- 👤 Complete user management (CRUD operations)
- 🛡️ Role-Based Access Control (RBAC)
- 🔄 Session management
- ✉️ Email verification
- 🔒 Password reset flow

### Advanced Features
- 📝 Fine-grained permission system
- 🏷️ Role hierarchy support
- 📊 Comprehensive audit logging
- ⚡ Optimized query performance
- 🐳 Docker container support

## Architecture Overview

## Getting Started

### Prerequisites
- .NET 9 SDK
- PostgreSQL 12+
- Docker (optional)

### Installation
1. Clone the repository:
```bash
git clone git@github.com:steve-bang/user-service-dot-net.git
cd UserService
```
2. Configure the application:
```bash
cp src/UserService.Api/appsettings.Example.json src/UserService.Api/appsettings.json
```
3. Update configuration:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=your-database;Username=user;Password=yourpassword"
  },
  "Jwt": {
    "Key": "your-secure-key-here",
    "Issuer": "user-service",
    "ExpiryInMinutes": 60
  }
}
```
4. Run database migrations:
```bash
dotnet ef database update
```
5. Seed initial data:
```bash
dotnet run seed
```
6. Run the application:
```bash
dotnet run
```

## Domain Model
### Key Entities
```CSharp
public class User : AggregateRoot
{
    public string FirstName { get; }
    public string LastName { get; }
    public EmailAddress Email { get; }
    public PasswordHash Password { get; }
    // ... other properties
}

public class Role : Entity
{
    public string Name { get; }
    public ICollection<Permission> Permissions { get; }
}

public class Permission : ValueObject
{
    public string Name { get; } // e.g. "users.create"
    public string Description { get; }
}
```
## Permission System
### Example Permissions
```CSharp
public static class SystemPermissions
{
    public static class Users
    {
        public const string View = "users.view";
        public const string Create = "users.create";
        public const string Update = "users.update";
        public const string Delete = "users.delete";
    }
    
    public static class Roles
    {
        public const string Assign = "roles.assign";
    }
}
```

### Usage Examples
1. Controller Authorization:
```CSharp
[Permission(SystemPermissions.Users.Delete)]
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteUser(Guid id)
```
2. Service Layer Check:
```CSharp
if (!await _authService.HasPermission(userId, "users.delete"))
{
    throw new ForbiddenException("Missing required permission");
}
```
### Testing
Run all tests:
```bash
dotnet test
```

## Deployment
### Docker
```bash
docker-compose -f docker-compose.yml -f docker-compose.prod.yml up -d --build
```
### Kubernetes
```bash
kubectl apply -f deploy/k8s/
```
## Contributing

1. Fork the repository
2. Create your feature branch (git checkout -b feature/AmazingFeature)
3. Commit your changes (git commit -m 'Add some feature')
4. Push to the branch (git push origin feature/AmazingFeature)
4. Open a Pull Request