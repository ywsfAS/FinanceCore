CREATE TABLE CreditDetails
(
    AccountId UNIQUEIDENTIFIER NOT NULL,

    CreditLimit DECIMAL(18, 2) NOT NULL,
    Fee DECIMAL(18, 2) NOT NULL,

    FeePeriodId INT NOT NULL,

    LastFeeChargedAt DATETIME2 NULL,
    NextFeeChargeAt DATETIME2 NULL,

    CONSTRAINT PK_CreditDetails
        PRIMARY KEY (AccountId),

    CONSTRAINT FK_CreditDetails_Accounts
        FOREIGN KEY (AccountId)
        REFERENCES Accounts(Id),

    CONSTRAINT CK_CreditDetails_CreditLimit
        CHECK (CreditLimit > 0),

    CONSTRAINT CK_CreditDetails_Fee
        CHECK (Fee >= 0)
);