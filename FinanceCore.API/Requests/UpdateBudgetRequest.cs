using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests
{
    public record UpdateBudgetRequest(
        string Name ,
        decimal Amount,
        EnCurrency Currency,
        EnPeriod Period,
        DateTime StartDate
       ); 
}
