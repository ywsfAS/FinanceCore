using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Transactions;
using MediatR;

namespace FinanceCore.Application.Features.Transactions.Commands.TransactionImports;

public sealed class Handler : IRequestHandler<Command>
{
    private readonly ITransactionParser<TransactionImport> _parser;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITransactionRepository _transactionRepository;

    public Handler(
        ITransactionParser<TransactionImport> parser,
        ICategoryRepository categoryRepository,
        ITransactionRepository transactionRepository)
    {
        _parser = parser;
        _categoryRepository = categoryRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task Handle(
        Command command,
        CancellationToken cancellationToken)
    {
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

        var transactions = new List<Transaction>();

        foreach (var import in imports)
        {
            if (string.IsNullOrWhiteSpace(import.Category))
            {
                continue;
            }

            if (!categoryIds.TryGetValue(
                    import.Category,
                    out var categoryId))
            {
                continue;
            }

            var transaction = Transaction.Create(
                command.AccountId,
                import.ToAccountId,
                new Money(import.Amount, import.Currency),
                categoryId,
                import.Type,
                import.Date,
                import.Description);

            transactions.Add(transaction);
        }

        if (transactions.Count == 0)
        {
            return;
        }

        await _transactionRepository.InsertTransactions(
            transactions,
            null,
            cancellationToken);
    }
}
