using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetSubscriptionsGrowth
{
    public record SubscriptionGrowthQuery(Guid UserId,Guid? AccountId, EnTransactionType Type , DateTime Start , DateTime End )  :IRequest<SubscriptionGrowthDto?>;
}
