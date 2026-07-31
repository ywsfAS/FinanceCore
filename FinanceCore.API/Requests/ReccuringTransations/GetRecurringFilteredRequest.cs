using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.ReccuringTransations
{
    public record GetRecurringFilteredRequest(Guid? AccountId , Guid? CategoryId , bool? IsActive , EnPeriod? Period , DateTime? Start , DateTime? End , int Page = 1 , int PageSize = 1 );
     
}
