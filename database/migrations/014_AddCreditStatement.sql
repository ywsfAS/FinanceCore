CREATE TABLE CreditStatements
(
    Id UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT PK_CreditStatements PRIMARY KEY,

    AccountId UNIQUEIDENTIFIER NOT NULL,

    PeriodStart DATETIME2 NOT NULL,
    PeriodEnd DATETIME2 NOT NULL,
    PaymentDueDate DATETIME2 NOT NULL,

    StatementBalance DECIMAL(18, 2) NOT NULL,
    MinimumPayment DECIMAL(18, 2) NOT NULL,
    PaidAmount DECIMAL(18, 2) NOT NULL,

    StatusId INT NOT NULL,

    CreatedAt DATETIME2 NOT NULL
        CONSTRAINT DF_CreditStatements_CreatedAt DEFAULT GETUTCDATE(),

    MinimumPaymentSatisfiedAt DATETIME2 NULL,
    PaidAt DATETIME2 NULL,

    CONSTRAINT FK_CreditStatements_Accounts
        FOREIGN KEY (AccountId)
        REFERENCES Accounts(Id),

    CONSTRAINT CK_CreditStatements_Period
        CHECK (PeriodEnd > PeriodStart),

    CONSTRAINT CK_CreditStatements_PaymentDueDate
        CHECK (PaymentDueDate >= PeriodEnd),

    CONSTRAINT CK_CreditStatements_Amounts
        CHECK (
            StatementBalance >= 0
            AND MinimumPayment >= 0
            AND PaidAmount >= 0
        ),

    CONSTRAINT CK_CreditStatements_MinimumPayment
        CHECK (MinimumPayment <= StatementBalance)
);