using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Abstractions
{
    public interface IExchangeRateApiService
    {
        Task<decimal> GetRateAsync(EnCurrency from, EnCurrency to, CancellationToken token);
    }
}
