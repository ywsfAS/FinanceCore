# FinanceCore - Domain-Driven Design Finance Application

A personal finance management system built with **Clean Architecture** and **Domain-Driven Design (DDD)** principles using **.NET 8**.

---

# Overview

FinanceCore helps users manage their personal finances by:

- Tracking income and expenses across multiple accounts
- Categorizing transactions for better insights
- Setting budgets to control spending
- Creating savings goals and tracking progress
- Managing finances in multiple currencies

The system follows **Clean Architecture** to keep the domain independent from infrastructure and frameworks, ensuring maintainability and testability.

---

# Architecture

The project follows **Clean Architecture + Domain Driven Design + CQRS**.

Key architectural patterns used:

- **Domain Driven Design (DDD)**
- **CQRS (Command Query Responsibility Segregation)**
- **Event-Driven Architecture**
- **Repository Pattern**
- **Decorator Pattern (Caching)**
- **MediatR Pipeline Behaviors**
- **Global Exception Handling**

---


#  Project Structure

```
FinanceCore/
│
├── FinanceCore.Domain/ # Core business logic
│ ├── Accounts/
│ ├── Transactions/
│ ├── RecurringTransactions/
│ ├── Users/
│ ├── Categories/
│ ├── Budgets/
│ ├── Goals/
│ ├── Common/ # Base classes (AggregateRoot, Entity, ValueObject)
│ ├── Events/ # Domain events
│ ├── Exceptions/ # Domain exceptions
│ └── Enums/ # Domain enums
│
├── FinanceCore.Application/ # Application use cases
│ ├── Abstractions/ # Interfaces & contracts
│ ├── DTOs/ # Data Transfer Objects
│ ├── Models/ # Query models
│ ├── Events/ # Event handlers
│ ├── Features/ # CQRS features (Accounts, Transactions, Categories, Budgets, Reports)
│ │ ├── Commands
│ │ ├── Queries
│ │ ├── Handlers
│ │ └── Validators
│ ├── ValidationBehavior.cs # MediatR pipeline validation
│ └── DependencyInjection.cs
│
├── FinanceCore.Infrastructure/ # External concerns
│ ├── Persistence/ # Repositories
│ ├── Context/ 
│ ├── BackgoundJobs/ #Quartz jobs (ex: Recurring transactions) 
│ ├── Mappers/ # Domain ↔ Persistence mapping
│ ├── Auth/ # JWT authentication
│ ├── Services/ # Infrastructure services (Email service)
│ └── DependencyInjection.cs
│
├── FinanceCore.API/ # Presentation Layer
│ ├── Controllers/ # REST API Controllers
│ ├── Requests/ # API request models
│ ├── GlobalExceptionMiddleware.cs
│ └── Program.cs
│
├── FinanceCore.Application.Tests/ # Application layer tests
├── DomainCore.Domain.Tests/ # Domain layer tests
│
├── CoinHive/ # Frontend React application (Typescript)
│ ├── src/
│ ├── public/
│ ├── package.json
│ └── README.md
│
└── README.md
```

# Domain Layer Components

The **Domain Layer** contains the core business logic and rules.

## Aggregates

### User

Represents a system user.

**Properties:**

- Name
- Email
- PasswordHash
- DefaultCurrency
- TimeZone

**Features:**

- Authentication
- Profile management
- Email verification
- Account lockout

---

### Account

Represents a financial account.

**Examples:** Checking, Savings, Credit, Cash, Investment

**Properties:**

- Name
- Type
- Currency
- Balance
- InitialBalance

**Features:**

- Balance tracking
- Transfers
- Account activation / deactivation

---

### Transaction

Represents a financial transaction.

**Properties:**

- Amount
- Type (Income / Expense)
- Date
- Description
- Category

**Features:**

- Create, update, and void transactions
- Maintain audit history

> Transactions are stored separately from accounts for **scalability and performance**.

---

### Category

Used to categorize transactions.

**Examples:**

- **Income:** Salary, Freelance, Investments
- **Expense:** Housing, Food, Transportation, Entertainment

**Properties:**

- Name
- Type
- Description

---

### Budget

Defines spending limits.

**Properties:**

- Amount
- SpentAmount
- Period (Weekly, Monthly, Quarterly, Yearly)

**Features:**

- Spending tracking
- Budget alerts and threshold notifications

---

### SavingsGoal

Represents financial goals.

**Examples:** Emergency Fund, Vacation, New Laptop

**Properties:**

- Name
- TargetAmount
- CurrentAmount
- TargetDate

**Features:**

- Progress tracking
- Contributions
- Milestones

---

# Value Objects

### Money

Represents an amount with currency.

**Properties:**

- Amount
- Currency

**Operations:**

- Add, Subtract, ConvertTo

---

### Email

Validated email value object.

**Validation:**

- Format
- Length
- Domain checks

---

# Domain Events

Domain events capture important state changes.

**Examples:**

- **User:** UserCreated, UserEmailChanged, UserLoggedIn, UserDeactivated
- **Account:** AccountCreated, AccountBalanceChanged, AccountTransfer
- **Transaction:** TransactionCreated, TransactionVoided, TransactionAmountChanged
- **Category:** CategoryCreated, CategoryUpdated, CategoryDeactivated
- **Budget:** BudgetCreated, BudgetExceeded, BudgetThresholdReached
- **Goal:** GoalCreated, GoalCompleted, GoalMilestoneReached

---

# Domain Exceptions

Custom exceptions for domain rule violations.

- **User:** UserNotFoundException, DuplicateEmailException, InvalidCredentialsException, UserAccountLockedException
- **Account:** AccountNotFoundException, InsufficientBalanceException, InactiveAccountException, CurrencyMismatchException
- **Transaction:** TransactionNotFoundException, VoidedTransactionException, InvalidTransactionAmountException
- **Category:** CategoryNotFoundException, DuplicateCategoryException, DefaultCategoryModificationException
- **Budget:** BudgetNotFoundException, BudgetExceededException, InvalidBudgetAmountException
- **Goal:** GoalNotFoundException, InvalidGoalAmountException, GoalAlreadyCompletedException

---

# Enums

- **Currency:** USD, EUR, GBP, MAD, etc.
- **AccountType:** Checking, Savings, Credit, Cash, Investment, Loan, Other
- **TransactionType:** Income, Expense
- **TransactionStatus:** Completed, Voided
- **CategoryType:** Income, Expense, Both
- **BudgetPeriod:** Weekly, Monthly, Quarterly, Yearly
- **GoalStatus:** Active, Paused, Completed, Cancelled

---

# Application Layer

Implements **CQRS** using **MediatR**.

Each feature contains:

- Commands
- Queries
- Handlers
- Validators

**Example structure:**
```
Features/
├── Accounts
│ ├── CreateAccount
│ ├── UpdateAccount
│ ├── DeleteAccount
│ └── GetAccountById
├── Transactions
├── Budgets
└── Reports
```

---
# Infrastructure Layer

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

# API Layer

Exposes the application through **REST endpoints**.

Contains:

- Controllers
- Request models
- Global exception middleware

**Example controllers:**

- AccountsController
- TransactionsController
- CategoriesController
- BudgetsController
- ReportsController
- AuthController
- UsersController

---

# Testing

### Domain Tests

Located in:
DomainCore.Domain.Tests


Tests:

- Domain rules
- Aggregates
- Value objects

### Application Tests

Located in:
FinanceCore.Application.Tests


Tests:

- Command handlers
- Query handlers
- Business workflows

---

# Key Design Decisions

## Account and Transaction Separation

Accounts maintain current balances, transactions store historical records.  
Improves scalability, simplifies reporting, and avoids heavy data loads.

---

## Categories

Categories organize transactions into logical groups, providing structured insights into spending behavior.

---

## Budgets

Budgets define spending limits for periods and categories, helping track planned vs actual spending.

---

## Savings Goals

Savings goals represent financial targets with progress tracking and milestones, providing a structured way to save.

---

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