using FinanceCore.Domain.Common;

namespace FinanceCore.Application.DTOs
{
    public record ReconciliationDto(Guid Id , Money ExpectedBalance , Money ActualBalance , Money difference , bool AdjustmentCreated , Guid? AdjustmentTransactionCreated );
}
