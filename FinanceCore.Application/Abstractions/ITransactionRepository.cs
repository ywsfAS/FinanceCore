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
        Task<IEnumerable<TransactionDto>?> GetFiltredTransactionsAsync(Guid? categoryId, DateTime? start, DateTime? end, byte? type, int page, int pageSize);
        Task<decimal> GetTotalSpentAsync(Guid categoryId, DateTime start, DateTime end, byte Type);
        Task AddAsync(Transaction transaction, CancellationToken token = default);
        Task UpdateAsync(Transaction transaction, CancellationToken token = default);
        Task DeleteAsync(Guid id, CancellationToken token = default);
        Task<Transaction?> GetByIdAndUserId(Guid userId , Guid id , CancellationToken token = default);
        Task<TransactionDto?> GetDtoByIdAndUserId(Guid userId , Guid id , CancellationToken token = default);
        Task<ReportModel?> GetMonthlySummary(Guid sccountId, DateTime start, DateTime end);
        Task<IEnumerable<TransactionDto>?> FetchTransactionsByIdPageAsync(Guid accountId, int page, int pageSize);
        Task<IEnumerable<SpendingByCategoryDto>> GetSpendingByCategory(
            Guid userId, Guid? accountId, DateTime start, DateTime end);
        Task<bool> IsExists(Guid userId, Guid id, CancellationToken token = default);
        Task<List<SpendingByCategoryDto>> GetSpendingByCategoryForUser(Guid userId , DateTime start , DateTime end);

    }
}
