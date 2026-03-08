# FinanceCore - Domain-Driven Design Finance Application

A personal finance management system built with **Clean Architecture** and **Domain-Driven Design (DDD)** principles using **.NET 8**.

---

# 📋 Overview

FinanceCore helps users manage their personal finances by:

- Tracking income and expenses across multiple accounts
- Categorizing transactions for better insights
- Setting budgets to control spending
- Creating savings goals and tracking progress
- Managing finances in multiple currencies

The system follows **Clean Architecture** to keep the domain independent from infrastructure and frameworks.

---

# 🚀 Architecture

The project follows **Clean Architecture + Domain Driven Design + CQRS**.

Key architectural patterns used:

- **Domain Driven Design (DDD)**
- **CQRS (Command Query Responsibility Segregation)**
- **Event Driven Architecture**
- **Repository Pattern**
- **Decorator Pattern (Caching)**
- **MediatR Pipeline Behaviors**
- **Global Exception Handling**

---

# 🏗️ Project Structure

```
FinanceCore/
│
├── FinanceCore.Domain/                 # Core business logic
│
│   ├── Accounts/
│   ├── Transactions/
│   ├── Users/
│   ├── Categories/
│   ├── Budgets/
│   ├── Goals/
│   │
│   ├── Common/                         # Base classes
│   │   ├── AggregateRoot
│   │   ├── Entity
│   │   └── ValueObject
│   │
│   ├── Events/                         # Domain events
│   ├── Exceptions/                     # Domain exceptions
│   └── Enums/                          # Domain enums
│
│
├── FinanceCore.Application/            # Application use cases
│
│   ├── Abstractions/                   # Interfaces & contracts
│   ├── DTOs/                           # Data Transfer Objects
│   ├── Models/                         # Query models
│   ├── Events/                         # Event handlers
│   │
│   ├── Features/                       # CQRS Features
│   │   ├── Accounts/
│   │   ├── Transactions/
│   │   ├── Categories/
│   │   ├── Budgets/
│   │   └── Reports/
│   │
│   │   Each feature contains:
│   │   - Commands
│   │   - Queries
│   │   - Handlers
│   │   - Validators
│   │
│   ├── ValidationBehavior.cs           # MediatR pipeline validation
│   └── DependencyInjection.cs
│
│
├── FinanceCore.Infrastructure/         # External concerns
│
│   ├── Persistence/                    # Repositories
│   ├── Context/                        # EF DbContext
│   ├── Mappers/                        # Domain ↔ Persistence mapping
│   ├── Auth/                           # JWT authentication
│   ├── Services/                       # Infrastructure services
│   └── DependencyInjection.cs
│
│
├── FinanceCore.API/                    # Presentation Layer
│
│   ├── Controllers/                    # REST API Controllers
│   ├── Requests/                       # API request models
│   ├── GlobalExceptionMiddleware.cs
│   └── Program.cs
│
│
├── FinanceCore.Application.Tests/      # Application layer tests
├── DomainCore.Domain.Tests/            # Domain tests
│
└── README.md
```

---

# 📦 Domain Layer Components

The **Domain Layer** contains the core business logic and rules.

## Aggregates

### 1️⃣ User

Represents a system user.

Properties:

- Name
- Email
- PasswordHash
- DefaultCurrency
- TimeZone

Features:

- Authentication
- Profile management
- Email verification
- Account lockout

---

### 2️⃣ Account

Represents a financial account.

Examples:

- Checking
- Savings
- Credit
- Cash
- Investment

Properties:

- Name
- Type
- Currency
- Balance
- InitialBalance

Features:

- Balance tracking
- Transfers
- Account activation / deactivation

---

### 3️⃣ Transaction

Represents a financial transaction.

Properties:

- Amount
- Type (Income / Expense)
- Date
- Description
- Category

Features:

- Create transactions
- Update transactions
- Void transactions
- Maintain audit history

Transactions are stored separately from accounts for **scalability and performance**.

---

### 4️⃣ Category

Used to categorize transactions.

Examples:

Income Categories

- Salary
- Freelance
- Investments

Expense Categories

- Housing
- Food
- Transportation
- Entertainment

Properties:

- Name
- Type
- Description

---

### 5️⃣ Budget

Defines spending limits.

Properties:

- Amount
- SpentAmount
- Period

Budget Periods:

- Weekly
- Monthly
- Quarterly
- Yearly

Features:

- Spending tracking
- Budget alerts
- Threshold notifications

---

### 6️⃣ SavingsGoal

Represents financial goals.

Examples:

- Emergency Fund
- Vacation
- New Laptop

Properties:

- Name
- TargetAmount
- CurrentAmount
- TargetDate

Features:

- Progress tracking
- Contributions
- Milestones

---

# 💎 Value Objects

### Money

Represents an amount with currency.

Properties:

- Amount
- Currency

Operations:

- Add
- Subtract
- ConvertTo

---

### Email

Validated email value object.

Validation:

- Format
- Length
- Domain checks

---

# ⚡ Domain Events

Domain events capture important state changes.

Examples:

### User Events

- UserCreated
- UserEmailChanged
- UserLoggedIn
- UserDeactivated

### Account Events

- AccountCreated
- AccountBalanceChanged
- AccountTransfer

### Transaction Events

- TransactionCreated
- TransactionVoided
- TransactionAmountChanged

### Category Events

- CategoryCreated
- CategoryUpdated
- CategoryDeactivated

### Budget Events

- BudgetCreated
- BudgetExceeded
- BudgetThresholdReached

### Goal Events

- GoalCreated
- GoalCompleted
- GoalMilestoneReached

---

# ⚠️ Domain Exceptions

Custom exceptions for domain rule violations.

### User

- UserNotFoundException
- DuplicateEmailException
- InvalidCredentialsException
- UserAccountLockedException

### Account

- AccountNotFoundException
- InsufficientBalanceException
- InactiveAccountException
- CurrencyMismatchException

### Transaction

- TransactionNotFoundException
- VoidedTransactionException
- InvalidTransactionAmountException

### Category

- CategoryNotFoundException
- DuplicateCategoryException
- DefaultCategoryModificationException

### Budget

- BudgetNotFoundException
- BudgetExceededException
- InvalidBudgetAmountException

### Goal

- GoalNotFoundException
- InvalidGoalAmountException
- GoalAlreadyCompletedException

---

# 🔢 Enums

### Currency

USD, EUR, GBP, MAD, etc.

### AccountType

- Checking
- Savings
- Credit
- Cash
- Investment
- Loan
- Other

### TransactionType

- Income
- Expense

### TransactionStatus

- Completed
- Voided

### CategoryType

- Income
- Expense
- Both

### BudgetPeriod

- Weekly
- Monthly
- Quarterly
- Yearly

### GoalStatus

- Active
- Paused
- Completed
- Cancelled

---

# 🧠 Application Layer

The Application Layer implements **CQRS** using **MediatR**.

Each feature contains:

- Commands
- Queries
- Handlers
- Validators

Example structure:

```
Features/
 ├── Accounts
 │    ├── CreateAccount
 │    ├── UpdateAccount
 │    ├── DeleteAccount
 │    └── GetAccountById
 │
 ├── Transactions
 │
 ├── Budgets
 │
 └── Reports
```

---

# ⚙️ Infrastructure Layer

Handles external concerns:

- Database persistence
- Authentication
- Caching
- Mapping
- External services

Components:

- **EF Core DbContext**
- **Repositories**
- **JWT Authentication**
- **Caching decorators**
- **Persistence mappers**

---

# 🌐 API Layer

The API layer exposes the application through **REST endpoints**.

Contains:

- Controllers
- Request models
- Global exception middleware

Example controllers:

- AccountsController
- TransactionsController
- CategoriesController
- BudgetsController
- ReportsController
- AuthController
- UsersController

---

# 🧪 Testing

The solution includes unit tests for:

### Domain Tests

```
DomainCore.Domain.Tests
```

Testing:

- Domain rules
- Aggregates
- Value objects

### Application Tests

```
FinanceCore.Application.Tests
```

Testing:

- Command handlers
- Query handlers
- Business workflows

# Key Design Decisions

## Account and Transaction Separation

Accounts maintain the current balance while transactions store historical financial records.  
This separation improves scalability, simplifies reporting, and avoids loading large transaction histories when only balances are required.

---

## Categories

Categories organize transactions into logical groups such as income and expenses.  
They provide structured insights into spending behavior and financial patterns.

---

## Budgets

Budgets define spending limits for specific periods and categories.  
They enable tracking planned versus actual spending and help maintain financial discipline.

---

## Savings Goals

Savings goals represent financial targets with progress tracking and milestones.  
They provide a structured way to save toward specific objectives.

---

## Event-Driven Design

Domain events capture significant state changes within the system.  
They support features such as auditing, notifications, and analytics while keeping domain logic decoupled.

---

# Future Improvements

Planned improvements include:

- Recurring transactions
- Financial analytics dashboards
- Shared budgets
- Mobile integration

The repository also includes **CoinHive**, a **TypeScript + React frontend project** that will integrate with the FinanceCore API.  
The frontend is currently in development and will be added soon.

---

# Author

FinanceCore is a personal backend architecture project demonstrating **Domain-Driven Design, Clean Architecture, and CQRS** using **.NET 8**.