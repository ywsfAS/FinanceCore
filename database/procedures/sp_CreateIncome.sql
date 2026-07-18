
CREATE PROCEDURE [dbo].[sp_CreateIncome]
(
    @AccountId UNIQUEIDENTIFIER,
    @CategoryId UNIQUEIDENTIFIER,
    @Amount DECIMAL(18,2),
    @Description NVARCHAR(255),
    @TransactionDate DATETIME2
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;


        UPDATE Accounts
        SET Balance = Balance + @Amount
        WHERE Id = @AccountId;

        DECLARE @TransactionId UNIQUEIDENTIFIER = NEWID();
        DECLARE @CurrencyId TINYINT;
        SELECT @CurrencyId = CurrencyId
        FROM Accounts
        WHERE Id = @AccountId;

        INSERT INTO Transactions
        (
            Id,
            AccountId,
            CategoryId,
            Amount,
            CurrencyId,
            TransactionTypeId,
            Date,
            CreatedAt,
            Description
        )
        VALUES
        (
            @TransactionId,
            @AccountId,
            @CategoryId,
            @Amount,
            @CurrencyId,
            0,
            @TransactionDate,
            SYSUTCDATETIME(),
            @Description
        );

        COMMIT TRANSACTION;


        SELECT
        t.Id,
        a.Name AS AccountName,
        ta.Name AS ToAccountName,
        c.Name AS CategoryName,
        t.Amount,
        a.CurrencyId AS Currency,
        t.TransactionTypeId AS Type,
        t.CreatedAt AS [Date],
        t.Description
        FROM Transactions t
        INNER JOIN Accounts a
            ON a.Id = t.AccountId
        LEFT JOIN Accounts ta
            ON ta.Id = t.ToAccountId
        LEFT JOIN Categories c
            ON c.Id = t.CategoryId
        WHERE t.Id = @TransactionId;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO