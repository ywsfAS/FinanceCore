
CREATE PROCEDURE [dbo].[sp_CreateExpense]
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
        SET Balance = Balance - @Amount
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
            CurrencyId,
            Amount,
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
            @CurrencyId,
            @Amount,
            1, 
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

CREATE PROCEDURE [dbo].[sp_Transfer]
(
    @SourceAccountId UNIQUEIDENTIFIER,
    @DestinationAccountId UNIQUEIDENTIFIER,
    @Amount DECIMAL(18,2),
    @Description NVARCHAR(250) = NULL,
    @TransactionDate DATETIME2
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY

        BEGIN TRANSACTION;

        DECLARE @SourceCurrencyId INT;
        DECLARE @DestinationCurrencyId INT;

        DECLARE @Rate DECIMAL(18,8) = 1;
        DECLARE @ConvertedAmount DECIMAL(18,2);

        SELECT @SourceCurrencyId = CurrencyId
        FROM Accounts
        WHERE Id = @SourceAccountId;

        SELECT @DestinationCurrencyId = CurrencyId
        FROM Accounts
        WHERE Id = @DestinationAccountId;

        IF @SourceCurrencyId <> @DestinationCurrencyId
        BEGIN

            SELECT TOP(1)
                @Rate = Rate
            FROM ExchangeRates
            WHERE SourceCurrencyId = @SourceCurrencyId
              AND TargetCurrencyId = @DestinationCurrencyId;

            IF @Rate IS NULL
                THROW 50001, 'Exchange rate not found.', 1;

            SET @ConvertedAmount = ROUND(@Amount * @Rate, 2);

        END
        ELSE
        BEGIN
            SET @ConvertedAmount = @Amount;
        END


        UPDATE Accounts
        SET Balance = Balance - @Amount
        WHERE Id = @SourceAccountId;

        UPDATE Accounts
        SET Balance = Balance + @ConvertedAmount
        WHERE Id = @DestinationAccountId;


        DECLARE @TransactionId UNIQUEIDENTIFIER = NEWID();

        INSERT INTO Transactions
        (
            Id,
            AccountId,
            ToAccountId,
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
            @SourceAccountId,
            @DestinationAccountId,
            @Amount,
            @SourceCurrencyId,
            2,
            @TransactionDate,
            SYSUTCDATETIME(),
            @Description
        );

        SELECT
            t.Id,
            sa.Name AS AccountName,
            da.Name AS ToAccountName,
            CAST(NULL AS NVARCHAR(100)) AS CategoryName,
            t.Amount,
            sa.CurrencyId AS Currency,
            t.TransactionTypeId AS Type,
            t.Date,
            t.Description
        FROM Transactions t
        INNER JOIN Accounts sa
            ON sa.Id = t.AccountId
        LEFT JOIN Accounts da
            ON da.Id = t.ToAccountId
        WHERE t.Id = @TransactionId;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH
END
GO

