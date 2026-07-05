# FinanceCore

FinanceCore is a personal finance management platform built as a layered, domain-driven solution with a .NET backend and a React/Vite frontend. The project is designed to explore clean architecture, CQRS, and modern web development practices for budgeting, account tracking, categories, goals, and reporting.

## Overview

FinanceCore aims to help users:

- Track incomes and expenses across accounts
- Organize transactions with categories
- Create and monitor budgets
- Set savings goals and follow progress
- Manage profile, authentication, and account settings

The repository currently combines:

- A backend API built with ASP.NET Core and .NET 8
- A frontend web application built with React, TypeScript, and Vite
- Domain and application-layer tests to support core business rules

## Project Status

This repository is an early-stage application with a solid architectural foundation, but it is not yet production-ready. The codebase demonstrates good separation of concerns and a clear domain model, while still needing hardening around security, testing, observability, and deployment readiness.

## Features

### Current capabilities

- User registration and authentication flows
- Profile management
- Budget creation, filtering, updating, and deletion
- Category and account-related domain concepts
- Domain-driven validation and exception handling
- Swagger/OpenAPI documentation for the API
- React-based UI with routing and client-side state via React Query

### Planned or future-facing areas

- Expanded reporting and analytics
- Transaction lifecycle management
- Better recurring transaction support
- Advanced authorization and role-based access
- Production deployment pipeline and monitoring

## Architecture Overview

The solution follows a layered architecture inspired by Clean Architecture and Domain-Driven Design.

- Domain layer: business entities, value objects, domain events, and domain rules
- Application layer: use cases, commands, queries, DTOs, validators, and MediatR orchestration
- Infrastructure layer: persistence, authentication, caching, repositories, external services, background jobs
- API layer: controllers, request models, middleware, and Swagger
- Frontend: React app with route-based pages, components, hooks, services, and context providers

## Solution Structure

```text
FinanceCore/
├── FinanceCore/                  # Domain project
├── FinanceCore.Application/     # Application services, DTOs, features, validators
├── FinanceCore.Infrastructure/  # Repositories, auth, jobs, persistence, services
├── FinanceCore.API/             # ASP.NET Core API host and controllers
├── FinanceCore.Application.Tests/
├── DomainCore.Domain.Tests/
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

- .NET 8
- ASP.NET Core Web API
- MediatR
- FluentValidation
- Dapper
- Quartz.NET
- JWT authentication
- Swagger / OpenAPI

### Frontend

- React 19
- TypeScript
- Vite
- React Router
- React Hook Form
- TanStack React Query
- Recharts and Chart.js
- Lucide icons

## Getting Started

### Prerequisites

- .NET SDK 8+
- Node.js 20+
- npm or pnpm
- A SQL-compatible database connection string for the backend

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

Frontend:
```bash
cd coinhive
npm run build
```

### Linting

```bash
cd coinhive
npm run lint
```

## Design Decisions

The current implementation favors:

- Strong domain modeling over anemic entities
- Separation of read and write concerns through the application layer
- Clear boundaries between infrastructure concerns and business logic
- A modern frontend stack with reusable UI components and data fetching abstractions

These choices provide a strong foundation for future growth, but they also highlight areas that still need strengthening before a production release.

## Screenshots

Screenshots will be added as the UI stabilizes. Placeholder locations for future visuals:

- docs/screenshots/dashboard.png
- docs/screenshots/budgets.png
- docs/screenshots/auth.png

## Testing

The repository already contains domain and application tests, which is a strong starting point. However, the project would benefit from broader coverage in the following areas:

- Backend integration tests for API endpoints
- Frontend component tests
- End-to-end user journey tests
- Contract testing for API responses

## License

This project is licensed under the MIT License. See the LICENSE file for details.


Budgets define spending limits for periods and categories, helping track planned vs actual spending.


## Event-Driven Design

Domain events capture significant state changes, enabling auditing, notifications, and analytics while keeping domain logic decoupled.

---

# Future Improvements

- Recurring transactions
- Financial analytics dashboards
- Shared budgets
- Mobile integration
- **CoinHive React frontend integration** (coming soon)

---

# Author

FinanceCore is a personal backend architecture project by me (Youssef AS) demonstrating **Domain-Driven Design, Clean Architecture, and CQRS**