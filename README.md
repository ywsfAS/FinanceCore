# FinanceCore - Domain-Driven Design Finance Application

A personal finance management system built with **Clean Architecture** and **Domain-Driven Design (DDD)** principles using **.NET 8**.

---

## 📋 Overview

FinanceCore helps users manage their personal finances by:
- Tracking income and expenses across multiple accounts
- Categorizing transactions for better insights
- Setting budgets to control spending
- Creating savings goals and tracking progress
- Managing finances in multiple currencies

**Built with:**
- Domain-Driven Design (DDD)
- Clean Architecture
- Event-Driven Architecture
- Rich Domain Models

---

## 🏗️ Project Structure
```
FinanceCore/
├── FinanceCore.Domain/          # Core business logic
│   ├── Accounts/                # Account aggregate
│   ├── Transactions/            # Transaction aggregate
│   ├── Users/                   # User aggregate
│   ├── Categories/              # Category aggregate
│   ├── Budgets/                 # Budget aggregate
│   ├── Goals/                   # Savings goal aggregate
│   ├── Common/                  # Base classes (AggregateRoot, Entity, ValueObject)
│   ├── Events/                  # Domain events
│   ├── Exceptions/              # Domain exceptions
│   └── Enums/                   # Domain enums
├── FinanceCore.Application/     # Use cases & application logic
├── FinanceCore.Infrastructure/  # Data access & external services
└── FinanceCore.API/             # REST API
```

---

## 📦 Domain Layer Components

### Aggregates

**1. User** - User identity and preferences
- Properties: Name, Email, PasswordHash, DefaultCurrency, TimeZone
- Features: Authentication, profile management, email verification, account lockout

**2. Account** - Financial accounts (checking, savings, credit, etc.)
- Properties: Name, Type, Currency, Balance, InitialBalance
- Features: Balance tracking, transfers, transactions, activation/deactivation

**3. Transaction** - Individual financial transactions
- Properties: Amount, Type (Income/Expense), Date, Description, Category
- Features: Create, update, void with audit trail
- Note: Separate aggregate from Account for better performance

**4. Category** - Transaction categorization
- Properties: Name, Type (Income/Expense/Both), Description
- Features: Organize spending, support subcategories, default system categories
- Purpose: Answer "Where does my money go?"

**5. Budget** - Spending limits and tracking
- Properties: Amount, SpentAmount, Period (Weekly/Monthly/Quarterly/Yearly)
- Features: Track spending, alerts at 80% and when exceeded
- Purpose: Control spending and live within means

**6. SavingsGoal** - Financial goals and progress tracking
- Properties: Name, TargetAmount, CurrentAmount, TargetDate
- Features: Contributions, withdrawals, milestone tracking, completion
- Purpose: Save for specific things with motivation

---

### Value Objects

**Money** - Encapsulates amount and currency
- Properties: Amount (decimal), Currency (enum)
- Methods: Add(), Subtract(), ConvertTo()

**Email** - Validated email address
- Properties: Address (string)
- Validation: Format, length, domain checks

---

### Domain Events

Events are raised when important state changes occur:

**User Events:** UserCreated, UserEmailChanged, UserLoggedIn, UserDeactivated, etc.
**Account Events:** AccountCreated, AccountBalanceChanged, AccountTransfer, etc.
**Transaction Events:** TransactionCreated, TransactionVoided, TransactionAmountChanged, etc.
**Category Events:** CategoryCreated, CategoryUpdated, CategoryDeactivated, etc.
**Budget Events:** BudgetCreated, BudgetExceeded, BudgetThresholdReached, etc.
**Goal Events:** GoalCreated, GoalCompleted, GoalMilestoneReached, etc.

---

### Domain Exceptions

Custom exceptions for precise error handling:

**User:** UserNotFoundException, DuplicateEmailException, InvalidCredentialsException, UserAccountLockedException
**Account:** AccountNotFoundException, InsufficientBalanceException, InactiveAccountException, CurrencyMismatchException
**Transaction:** TransactionNotFoundException, VoidedTransactionException, InvalidTransactionAmountException
**Category:** CategoryNotFoundException, DuplicateCategoryException, DefaultCategoryModificationException
**Budget:** BudgetNotFoundException, BudgetExceededException, InvalidBudgetAmountException
**Goal:** GoalNotFoundException, InvalidGoalAmountException, GoalAlreadyCompletedException

---

### Enums

**Currency:** USD, EUR, GBP, MAD, etc.
**AccountType:** Checking, Savings, Credit, Cash, Investment, Loan, Other
**TransactionType:** Income, Expense
**TransactionStatus:** Completed, Voided
**CategoryType:** Income, Expense, Both
**BudgetPeriod:** Weekly, Monthly, Quarterly, Yearly
**GoalStatus:** Active, Paused, Completed, Cancelled

---

## 🎯 Key Design Decisions

### 1. Account and Transaction Separation
- **Account** maintains current balance (aggregate root)
- **Transaction** stores historical records (separate aggregate root)
- Linked by `AccountId` foreign key
- Benefits: Better performance, scalability, and query flexibility

### 2. Category Purpose
Categories answer: "Where is my money going?"
- Income categories: Salary, Freelance, Investments
- Expense categories: Housing, Food, Transportation, Entertainment

### 3. Budget Purpose
Budgets answer: "How much should I spend?"
- Set spending limits per category
- Get alerts at 80% usage and when exceeded
- Track actual vs planned spending

### 4. Goal Purpose
Goals answer: "What am I saving for?"
- Specific targets with progress tracking
- Milestone celebrations (25%, 50%, 75%, 100%)
- Visual motivation to save

### 5. Event-Driven Architecture
- Domain events capture all important state changes
- Enable audit trails, notifications, analytics
- Support eventual consistency patterns

### 6. Rich Domain Models
- Business logic lives in the domain entities
- Entities protect their invariants
- Factory methods ensure valid creation
- No anemic domain models