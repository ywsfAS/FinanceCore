CREATE TABLE AdjustmentStatus
(
    Id INT NOT NULL PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);

INSERT INTO AdjustmentStatus (Id, Name)
VALUES
(0, 'None'),
(1, 'Applied');

CREATE TABLE Reconciliations
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

    AccountId UNIQUEIDENTIFIER NOT NULL,

    ExpectedBalance DECIMAL(18,2) NOT NULL,
    ActualBalance DECIMAL(18,2) NOT NULL,

    Reason NVARCHAR(255) NOT NULL,

    Notes NVARCHAR(500) NULL,

    AdjustmentStatusId INT NOT NULL,

    AdjustmentTransactionId UNIQUEIDENTIFIER NULL,

    ReconciledAt DATETIME2 NOT NULL,

    CreatedAt DATETIME2 NOT NULL
        CONSTRAINT DF_Reconciliations_CreatedAt
        DEFAULT GETUTCDATE(),

    CONSTRAINT FK_Reconciliations_Accounts
        FOREIGN KEY (AccountId)
        REFERENCES Accounts(Id),

    CONSTRAINT FK_Reconciliations_Transactions
        FOREIGN KEY (AdjustmentTransactionId)
        REFERENCES Transactions(Id),

    CONSTRAINT FK_Reconciliations_AdjustmentStatus
        FOREIGN KEY (AdjustmentStatusId)
        REFERENCES AdjustmentStatus(Id)
);

CREATE INDEX IX_Reconciliations_AccountId
ON Reconciliations(AccountId);

CREATE INDEX IX_Reconciliations_ReconciledAt
ON Reconciliations(ReconciledAt);

INSERT INTO TransactionTypes (TransactionTypeId, Code, Name)
VALUES
(5, 'CreditAdjustment', 'Credit Adjustment Transaction'),
(6, 'DebitAdjustment', 'Debit Adjustment Transaction');