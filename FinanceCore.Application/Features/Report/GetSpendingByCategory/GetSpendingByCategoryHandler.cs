using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Report.GetSpendingByCategory
{
    public class GetSpendingByCategoryHandler
        : IRequestHandler<GetSpendingByCategoryQuery, IEnumerable<SpendingByCategoryDto>?>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetSpendingByCategoryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<SpendingByCategoryDto>?> Handle(
            GetSpendingByCategoryQuery query,
            CancellationToken token)
        {
            var startDate = new DateTime(query.Year, query.Month, 1);
            var endDate = startDate.AddMonths(1); 
            var result = await _transactionRepository
                .GetSpendingByCategoryAsync(
                    query.UserId,
                    query.AccountId,
                    startDate,
                    endDate,
                    query.Page,
                    query.PageSize
                    );

            return result;
        }
    }
}
