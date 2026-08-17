-- Loan contract information for loan accounts
CREATE TABLE LoanDetails
(
    AccountId UNIQUEIDENTIFIER NOT NULL,

    PrincipalAmount DECIMAL(18,2) NOT NULL,
    InterestRate DECIMAL(5,4) NOT NULL,
    TermInMonths INT NOT NULL,
    RepaymentFrequency TINYINT NOT NULL,

    StartDate DATETIME2 NOT NULL,
    MaturityDate DATETIME2 NOT NULL,

    RegularPaymentAmount DECIMAL(18,2) NOT NULL,
    NextPaymentDate DATETIME2 NULL,

    CONSTRAINT PK_LoanDetails
        PRIMARY KEY (AccountId),

    CONSTRAINT FK_LoanDetails_Accounts
        FOREIGN KEY (AccountId)
        REFERENCES Accounts(Id),

    CONSTRAINT CK_LoanDetails_PrincipalAmount
        CHECK (PrincipalAmount > 0),

    CONSTRAINT CK_LoanDetails_InterestRate
        CHECK (InterestRate >= 0),

    CONSTRAINT CK_LoanDetails_TermInMonths
        CHECK (TermInMonths > 0),

    CONSTRAINT CK_LoanDetails_RegularPaymentAmount
        CHECK (RegularPaymentAmount > 0),

    CONSTRAINT CK_LoanDetails_MaturityDate
        CHECK (MaturityDate >= StartDate)
);
