using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Report.GetBudgetHealth
{
    public class BudgetHealthHandler :IRequestHandler<BudgetHealthQuery,BudgetHealthDto?>
    {

        private readonly ITransactionRepository _transactionRepository;
        private readonly ICurrencyConverter _currencyConverter;
        public BudgetHealthHandler(ITransactionRepository transactionRepository, ICurrencyConverter currencyConverter)
        {
            _transactionRepository = transactionRepository;
            _currencyConverter = currencyConverter;
        }
        public async Task<BudgetHealthDto?> Handle(BudgetHealthQuery query, CancellationToken token)
        {
            var budgets = await _transactionRepository.GetBudgetHealthAsync(query.UserId,query.Page,query.PageSize);
            if (budgets is null) return null ;
            EnCurrency globalCurreny = EnCurrency.USD;
            decimal totalSpent = 0;
            decimal totalBudget = 0;
            foreach (var item in budgets) {
                var budget = new Budget
                {
                    Amount = new Money(item.Amount, item.Currency),
                    Spent = new Money(item.Spent, item.Currency)
                };


                item.Status = budget.ComputeBudgetHealth();

            }
            var conversionTasks = budgets.Select(async (item) =>
            {
                var convertedAmountTasks = _currencyConverter.Convert(item.Amount, item.Currency, globalCurreny, token);
                var convertedSpentTasks = _currencyConverter.Convert(item.Spent, item.Currency, globalCurreny, token);
                await Task.WhenAll(convertedAmountTasks, convertedSpentTasks);
                return (Amount: await convertedAmountTasks, Spent: await convertedSpentTasks);
            });
            var convertedResults = await Task.WhenAll(conversionTasks);

            totalBudget += convertedResults.Sum(r => r.Amount);
            totalSpent += convertedResults.Sum(r => r.Spent);
            var globalBudget = new Budget
            {
                Amount = new Money(totalBudget, globalCurreny),
                Spent = new Money(totalSpent, globalCurreny)
            };
            var score = globalBudget.ComputeHealthScore();
            return new BudgetHealthDto(query.UserId, score, budgets, new PaginationDto(budgets.Count(), query.Page, query.PageSize));

        }
    }
}
