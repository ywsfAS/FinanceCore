using FluentValidation;

namespace FinanceCore.Application.Features.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQueryValidator : AbstractValidator<GetTransactionByIdQuery>
    {

        public GetTransactionByIdQueryValidator() { }
    }
}
