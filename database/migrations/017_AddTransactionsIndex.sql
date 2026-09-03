CREATE INDEX IX_Transactions_AccountId_CreatedAt
ON Transactions(AccountId, CreatedAt)
INCLUDE (Amount, TransactionTypeId);