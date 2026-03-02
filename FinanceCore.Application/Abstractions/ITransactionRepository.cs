using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Abstractions
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid id, CancellationToken token = default);
        Task<CreateTransferDto> TransferAsync(Transaction transaction, CancellationToken token = default);
        Task<CreateTransactionDto> IncomeAsync(Transaction transaction, CancellationToken token);
        Task<CreateTransactionDto> ExpenseAsync(Transaction transaction, CancellationToken token);
        Task<IEnumerable<TransactionDto>?> GetFiltredTransactionsAsync(Guid? CategoryId, DateTime? Start, DateTime? End, byte? Type, int Page, int PageSize);
        Task<decimal> GetTotalSpentAsync(Guid categoryId, DateTime start, DateTime end, byte Type);
        Task AddAsync(Transaction transaction, CancellationToken token = default);
        Task UpdateAsync(Transaction transaction, CancellationToken token = default);
        Task DeleteAsync(Transaction transaction, CancellationToken token = default);
        Task<Transaction?> GetByIdAndUserId(Guid UserId , Guid Id , CancellationToken token = default);
        Task<TransactionDto?> GetDtoByIdAndUserId(Guid UserId , Guid Id , CancellationToken token = default);
        Task<ReportModel?> GetMonthlySummary(Guid AccountId, DateTime Start, DateTime End);
        Task<IEnumerable<TransactionDto>?> FetchTransactionsByIdPageAsync(Guid AccountId, int Page, int PageSize);
        Task<IEnumerable<SpendingByCategoryDto>> GetSpendingByCategory(
            Guid userId, Guid? accountId, DateTime start, DateTime end);

    }
}
