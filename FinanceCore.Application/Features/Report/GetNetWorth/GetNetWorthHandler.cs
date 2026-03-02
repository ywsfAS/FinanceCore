using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Report.GetNetWorth;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class GetNetWorthHandler : IRequestHandler<GetNetWorthQuery, NetWorthDto?>
{
    private readonly IAccountRepository _accountRepository;

    public GetNetWorthHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<NetWorthDto?> Handle(GetNetWorthQuery query, CancellationToken token)
    {
        var totalBalance = await _accountRepository.GetTotalBalanceAsync(query.UserId);

        return new NetWorthDto(totalBalance);
    }
}
