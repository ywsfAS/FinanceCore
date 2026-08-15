-- Alerts table
CREATE TABLE Alerts
(
    Id UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT PK_Alerts PRIMARY KEY,

    AccountId UNIQUEIDENTIFIER NOT NULL,

    ThresholdAmount DECIMAL(18,2) NOT NULL,

    IsEnabled BIT NOT NULL
        CONSTRAINT DF_Alerts_IsEnabled DEFAULT 0,

    LastTriggeredAt DATETIME2 NULL,

    CreatedAt DATETIME2 NOT NULL
        CONSTRAINT DF_Alerts_CreatedAt DEFAULT GETUTCDATE(),

    UpdatedAt DATETIME2 NULL,

    CONSTRAINT FK_Alerts_Accounts
        FOREIGN KEY (AccountId)
        REFERENCES Accounts(Id),

    CONSTRAINT CK_Alerts_ThresholdAmount
        CHECK (ThresholdAmount > 0)
);