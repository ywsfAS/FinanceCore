-- drop (UserId,Name) unique constraint
ALTER TABLE Categories
DROP CONSTRAINT UQ_Categories_UserId_Name

-- drop user foreign key constraint 
ALTER TABLE Categories
DROP CONSTRAINT FK_Categories_Users

-- make userId nullable for system level categories
ALTER TABLE Categories
ALTER COLUMN UserId UNIQUEIDENTIFIER NULL;

-- make a foreign key constraint 
ALTER TABLE Categories
ADD CONSTRAINT FK_Categories_Users
        FOREIGN KEY (UserId)
        REFERENCES Users(Id)

-- add a system column
ALTER TABLE Categories
ADD IsSystem BIT NOT NULL
CONSTRAINT DF_Categories_IsSystem DEFAULT (0)

-- add back (UserId,Name)
ALTER TABLE Categories
ADD CONSTRAINT UQ_Categories_UserId_Name
UNIQUE (UserId,Name)

-- seed categories (Cash Adjustment)
INSERT INTO Categories
(
    Id,
    UserId,
    Name,
    CategoryTypeId,
    Description,
    IsActive,
    IsDefault,
    IsSystem
)
VALUES
(
    '11111111-1111-1111-1111-111111111111',
    NULL,
    'Credit Adjustment',
    0, -- Income
    'System category for reconciliation balance increases.',
    1,
    1,
    1
),
(
    '22222222-2222-2222-2222-222222222222',
    NULL,
    'Debit Adjustment',
    1, -- Expense
    'System category for reconciliation balance decreases.',
    1,
    1,
    1
);
-- fix mark reconciledAt as nullable
ALTER TABLE Reconciliations
ALTER COLUMN ReconciledAt DATETIME2 NULL;