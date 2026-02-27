using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Commands.Create
{
    public record TransferTransactionCommand(Guid accountId ,Guid ToAccountId, decimal amount , string? description = null , string? notes = null) : IRequest<TransferDto>;
}
