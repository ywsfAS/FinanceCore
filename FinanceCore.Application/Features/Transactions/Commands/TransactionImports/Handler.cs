using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Batch;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.Transactions;
using MediatR;

namespace FinanceCore.Application.Features.Transactions.Commands.TransactionImports;

public sealed class Handler : IRequestHandler<ImportTransactionCommand>
{
    private readonly ITransactionParser<TransactionImport> _parser;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IBatchRepository _batchRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public Handler(
        ITransactionParser<TransactionImport> parser,
        ICategoryRepository categoryRepository,
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository,
        IBatchRepository batchRepository,
        IUnitOfWork unitOfWork)
    {
        _parser = parser;
        _categoryRepository = categoryRepository;
        _transactionRepository = transactionRepository;
        _batchRepository = batchRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        ImportTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var userAccounts =
            await _accountRepository.GetUserOwnedAccountsAsync(
                command.UserId,
                cancellationToken);
        
        if (!userAccounts.TryGetValue(command.AccountId,out var account))
        {
            throw new AccountNotFoundException(command.AccountId);
        }

        var imports = _parser.Parse(command.Stream);

        var categoryNames = imports
            .Select(x => x.Category)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var categoryIds = await _categoryRepository.ResolveCategoriesId(
            command.UserId,
            categoryNames,
            cancellationToken);

        var batch = new Batch
        {
            AccountId = command.AccountId,
            FileName = command.FileName,
            ImportedAt = DateTime.UtcNow
        };

        var transactions = new List<Transaction>();

        // Keeps track of accounts whose balances were modified to bulk update them after.
        var modifiedAccounts = new Dictionary<Guid, Account>();

        foreach (var import in imports)
        {
            var money = new Money(
                import.Amount,
                import.Currency);

            Guid? categoryId = null;

            if (import.Type == EnTransactionType.Transfer)
            {
                if (import.ToAccountId is null || !userAccounts.TryGetValue(import.ToAccountId.Value, out var toAccount)) continue; 

                account.TransferTo(
                    toAccount,
                    money);

                modifiedAccounts[account.Id] = account;
                modifiedAccounts[toAccount.Id] = toAccount;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(import.Category) || !categoryIds.TryGetValue(import.Category, out var resolvedCategoryId)) continue;

                categoryId = resolvedCategoryId;

                account.ApplyTransaction(
                    money,
                    import.Type);

                modifiedAccounts[account.Id] = account;
            }

            var transaction = Transaction.Create(
                accountId: command.AccountId,
                toAccountId: import.ToAccountId,
                amount: money,
                categoryId: categoryId,
                type: import.Type,
                date: import.Date,
                description: import.Description,
                batchId: batch.Id);

            transactions.Add(transaction);
        }

        if (transactions.Count == 0)
        {
            return;
        }

        batch.TransactionCount = transactions.Count;

        await _unitOfWork.BeginAsync(cancellationToken);

        try
        {
            await _batchRepository.AddAsync(
                batch,
                _unitOfWork,
                cancellationToken);

            await _transactionRepository.InsertTransactions(
                transactions,
                _unitOfWork,
                cancellationToken);

            await _accountRepository.UpdateAccountsAsync(modifiedAccounts.Values,_unitOfWork, cancellationToken);

            await _unitOfWork.CommitAsync(
                cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollBackAsync(
                cancellationToken);

            throw;
        }
    }
}