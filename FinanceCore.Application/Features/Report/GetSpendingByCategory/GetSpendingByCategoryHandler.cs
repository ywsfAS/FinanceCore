using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetSpendingByCategory
{
    public class GetSpendingByCategoryHandler
        : IRequestHandler<GetSpendingByCategoryQuery, IEnumerable<SpendingByCategoryDto>?>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public GetSpendingByCategoryHandler(
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<SpendingByCategoryDto>?> Handle(
            GetSpendingByCategoryQuery query,
            CancellationToken token)
        {
            // 1️⃣ Validate account ownership (optional if accountId provided)
            if (query.AccountId.HasValue)
            {
                var account = await _accountRepository
                    .GetDtoByIdAndUserIdAsync(query.UserId, query.AccountId.Value);

                if (account == null)
                    return null;             }
            var startDate = new DateTime(query.year, query.month, 1);
            var endDate = startDate.AddMonths(1); // entire month
            var result = await _transactionRepository
                .GetSpendingByCategory(
                    query.UserId,
                    query.AccountId,
                    startDate,
                    endDate);

            return result ?? new List<SpendingByCategoryDto>();
        }
    }
}
