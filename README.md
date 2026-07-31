# FinanceCore

FinanceCore is a personal finance management platform built as a layered, domain-driven solution with a **.NET 8 backend**, **SQL Server database**, and **React/Vite frontend**. The project is designed to explore **Clean Architecture, Domain-Driven Design, CQRS, and modern backend development practices** for budgeting, account tracking, transactions, savings goals, and reporting.

## Overview

FinanceCore aims to help users:

* Track incomes and expenses across accounts
* Transfer money between accounts
* Organize transactions with categories
* Create and monitor budgets
* Set savings goals and follow progress
* Export filtered transactions to CSV
* Manage profiles and profile images
* Manage authentication and account settings

The repository currently combines:

* A backend API built with **ASP.NET Core and .NET 8**
* A **SQL Server** database with SQL-first migrations
* A frontend web application built with **React, TypeScript, and Vite**
* Domain, application, and integration tests

## Project Status

FinanceCore is an actively developed project focused on building a maintainable and secure backend architecture.

The project currently includes:

* Clean Architecture and Domain-Driven Design
* CQRS with MediatR
* Repository and Unit of Work patterns
* SQL-first database migrations with DbUp
* Optimistic concurrency and transactional persistence
* Domain events
* JWT authentication and refresh tokens
* Role-based authorization
* Account lockout and brute-force protection
* Secure profile image processing and storage
* In-memory caching and cache invalidation
* API rate limiting and health checks
* Global exception handling with ProblemDetails
* Serilog logging
* Domain, application, and integration testing

## Features

### Current capabilities

* User registration and authentication
* JWT access tokens and refresh tokens
* Refresh token rotation and revocation
* Logout and logout-all sessions
* Role-based authorization
* Account lockout and brute-force protection
* Profile management and avatar uploads
* Account and transaction management
* Income, expense, and transfer operations
* Budget creation, filtering, updating, and deletion
* Savings goals
* Category-based financial organization
* CSV transaction export
* In-memory caching with cache invalidation
* Domain event handling
* API rate limiting
* Health checks
* Swagger/OpenAPI documentation
* React-based UI with TanStack React Query

## Architecture Overview

The solution follows a layered architecture inspired by **Clean Architecture** and **Domain-Driven Design**.

* **Domain layer:** business entities, aggregates, value objects, domain events, and business rules
* **Application layer:** commands, queries, DTOs, validators, use cases, and MediatR orchestration
* **Infrastructure layer:** Dapper/ADO.NET persistence, repositories, authentication, caching, file storage, and external services
* **API layer:** controllers, middleware, request models, rate limiting, health checks, and Swagger
* **Frontend:** React application with route-based pages, components, hooks, services, and client-side data fetching

## Solution Structure

```text
FinanceCore/
├── FinanceCore/                  # Domain project
├── FinanceCore.Application/     # Application services, DTOs, features, validators
├── FinanceCore.Infrastructure/  # Repositories, auth, caching, storage, persistence
├── FinanceCore.API/             # ASP.NET Core API host and controllers
├── FinanceCore.Database/        # Database-related functionality
├── FinanceCore.Application.Tests/
├── DomainCore.Domain.Tests/
├── FinanceCore.Integration.Tests/
├── database/
│   ├── migrations/              # SQL-first database migrations
│   └── procedures/              # SQL Server stored procedures
├── coinhive/                    # React + TypeScript frontend
│   ├── src/
│   │   ├── components/
│   │   ├── context/
│   │   ├── hooks/
│   │   ├── pages/
│   │   ├── services/
│   │   └── use-cases/
│   └── package.json
└── README.md
```

## Technologies Used

### Backend

* .NET 8
* ASP.NET Core Web API
* MediatR
* FluentValidation
* Dapper
* ADO.NET
* SQL Server
* DbUp
* Quartz.NET
* JWT Authentication
* Serilog
* Swagger / OpenAPI

### Frontend

* React 19
* TypeScript
* Vite
* React Router
* React Hook Form
* TanStack React Query
* Recharts and Chart.js
* Lucide icons

### Testing

* xUnit
* Domain tests
* Application tests
* Integration tests

## Getting Started

### Prerequisites

* .NET SDK 8+
* Node.js 20+
* npm or pnpm
* SQL Server
* A configured database connection string

### Backend setup

1. Restore NuGet packages:

   ```bash
   dotnet restore FinanceCore.sln
   ```

2. Configure application settings, including connection strings and JWT settings.

3. Run the API:

   ```bash
   dotnet run --project FinanceCore.API
   ```

4. Open Swagger at:

   ```text
   https://localhost:<port>/swagger
   ```

### Frontend setup

1. Change into the frontend folder:

   ```bash
   cd coinhive
   ```

2. Install dependencies:

   ```bash
   npm install
   ```

3. Start the development server:

   ```bash
   npm run dev
   ```

4. Open the app at:

   ```text
   http://localhost:5173
   ```

## Development

### Build commands

Backend:

```bash
dotnet build FinanceCore.sln
```

Run tests:

```bash
dotnet test FinanceCore.sln
```

Frontend:

```bash
cd coinhive
npm run build
```

Lint:

```bash
cd coinhive
npm run lint
```



## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Author

FinanceCore is a personal backend architecture project by **Youssef AS**, demonstrating **Domain-Driven Design, Clean Architecture, CQRS, Event-Driven Architecture, and secure .NET backend development**.
