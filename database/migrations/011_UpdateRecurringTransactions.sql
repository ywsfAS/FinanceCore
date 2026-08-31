-- add status , execution type , nextExecutedAt
ALTER TABLE RecurringTransactions
ADD Status TINYINT NOT NULL
CONSTRAINT DF_RecurringTransactions_Status DEFAULT (0)

ALTER TABLE RecurringTransactions
ADD ExecutionType TINYINT NOT NULL
CONSTRAINT DF_RecurringTransactions_Execution_Type DEFAULT (0)

ALTER TABLE RecurringTransactions
ADD NextExecutionAt DATETIME2 NULL

ALTER TABLE RecurringTransactions
DROP COLUMN IsActive
