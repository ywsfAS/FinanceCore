using FluentValidation;

namespace FinanceCore.Application.Features.Transactions.Queries.GetFiltredTransactions
{
    public class GetFiltredTransactionsValidator : AbstractValidator<GetFiltredTransactionsQuery>
    {
        public GetFiltredTransactionsValidator() {
            RuleFor(x => x.Page).GreaterThan(0).NotEmpty();
            RuleFor(x => x.PageSize).GreaterThan(0).NotEmpty();
        }
    }
}
