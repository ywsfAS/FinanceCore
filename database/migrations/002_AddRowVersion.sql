ALTER TABLE Accounts
ADD RowVersion rowversion NOT NULL;

ALTER TABLE Budgets
ADD RowVersion rowversion NOT NULL;