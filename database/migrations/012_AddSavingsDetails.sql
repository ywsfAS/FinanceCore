-- interst infos for savings accounts
CREATE TABLE SavingsDetails(
    AccountId UNIQUEIDENTIFIER NOT NULL,
    InterestRate DECIMAL(5,4) NOT NULL,
    LastInterestAccrualAt DATETIME2 NULL,
    NextInterestCreditAt DATETIME2 NULL,
    CreditFrequency TINYINT NOT NULL,
    InterestAccruedToDate DECIMAL(18,2) NOT NULL
        CONSTRAINT DF_SavingsDetails_InterestAccruedToDate DEFAULT (0),

    CONSTRAINT PK_SavingsDetails
        PRIMARY KEY (AccountId),

    CONSTRAINT FK_SavingsDetails_Account
        FOREIGN KEY (AccountId)
        REFERENCES Accounts(Id),

    CONSTRAINT CK_SavingsDetails_InterestRate
        CHECK (InterestRate >= 0),

    CONSTRAINT CK_SavingsDetails_InterestAccruedToDate
        CHECK (InterestAccruedToDate >= 0)
    
    CONSTRAINT CK_SavingsDetails_CreditFrequency
        CHECK (CreditFrequency >= 0)


);