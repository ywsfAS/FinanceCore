
INSERT INTO AccountTypes (AccountTypeId,Code,Name)
VALUES (0,'Checking','Checking Account')
INSERT INTO AccountTypes (AccountTypeId,Code,Name)
VALUES (1,'Savings','Savings Account')
INSERT INTO AccountTypes (AccountTypeId,Code,Name)
VALUES (2,'Credit','Credit Card')
INSERT INTO AccountTypes (AccountTypeId,Code,Name)
VALUES (3,'Investment','Investment Account')
INSERT INTO AccountTypes (AccountTypeId,Code,Name)
VALUES (4,'Cash','Cash')

INSERT INTO BudgetPeriods (BudgetPeriodId,Code,Name)
VALUES (0,'Daily','Daily Budget')
INSERT INTO BudgetPeriods (BudgetPeriodId,Code,Name)
VALUES (1,'Weekly','Weekly Budget')
INSERT INTO BudgetPeriods (BudgetPeriodId,Code,Name)
VALUES (2,'Monthly','Monthly Budget')
INSERT INTO BudgetPeriods (BudgetPeriodId,Code,Name)
VALUES (3,'Yearly','Yearly Budget')

INSERT INTO CategoryTypes (CategoryTypeId , Code , Name)
VALUES (0,'Income','Income Category')
INSERT INTO CategoryTypes (CategoryTypeId , Code , Name)
VALUES (1,'Expense','Expense Category')

INSERT INTO ContributionsTypes (Id , Name)
VALUES (0,'Contribution')
INSERT INTO ContributionsTypes (Id , Name)
VALUES (1,'Withdrawal')
INSERT INTO ContributionsTypes (Id , Name)
VALUES (2,'Interest')
INSERT INTO ContributionsTypes (Id , Name)
VALUES (3,'Bonus')

INSERT INTO Currencies (CurrencyId ,Code ,Name)
VALUES (0,'USD','US Dollar')
INSERT INTO Currencies (CurrencyId ,Code ,Name)
VALUES (1,'EUR','Euro')
INSERT INTO Currencies (CurrencyId ,Code ,Name)
VALUES (2,'GBP','British Pound')
INSERT INTO Currencies (CurrencyId ,Code ,Name)
VALUES (3,'JPY','Japanese Yen')
INSERT INTO Currencies (CurrencyId ,Code ,Name)
VALUES (4,'CAD','Canadian Dollar')
INSERT INTO Currencies (CurrencyId ,Code ,Name)
VALUES (5,'AUD','Australian Dollar')
INSERT INTO Currencies (CurrencyId ,Code ,Name)
VALUES (6,'CHF','Swiss Franc')
INSERT INTO Currencies (CurrencyId ,Code ,Name)
VALUES (7,'CNY','Chinese Yuan')
INSERT INTO Currencies (CurrencyId ,Code ,Name)
VALUES (8,'MXN','Mexican Peso')
INSERT INTO Currencies (CurrencyId ,Code ,Name)
VALUES (9,'INR','Indian Rupee')

INSERT INTO GoalStatuses (Id , Name)
VALUES (0,'Active')
INSERT INTO GoalStatuses (Id , Name)
VALUES (1,'Paused')
INSERT INTO GoalStatuses (Id , Name)
VALUES (2,'Completed')
INSERT INTO GoalStatuses (Id , Name)
VALUES (3,'Cancelled')

INSERT INTO TransactionTypes (TransactionTypeId,Code,Name)
VALUES (0,'Income','Income Transaction')
INSERT INTO TransactionTypes (TransactionTypeId,Code,Name)
VALUES (1,'Expense','Expense Transaction')
INSERT INTO TransactionTypes (TransactionTypeId,Code,Name)
VALUES (2,'Transfer','Transfer Transaction')
INSERT INTO TransactionTypes (TransactionTypeId,Code,Name)
VALUES (3,'Debt','Debt Transaction')
INSERT INTO TransactionTypes (TransactionTypeId,Code,Name)
VALUES (4,'Credit','Credit Transaction')


SET IDENTITY_INSERT MessageSubjects ON;
INSERT INTO MessageSubjects (Id, Name)
VALUES
    (1, 'Account and Billing'),
    (2, 'Technical Support'),
    (3, 'Feature Request'),
    (4, 'Partnership'),
    (5, 'Security'),
    (6, 'Other');
SET IDENTITY_INSERT MessageSubjects OFF;
