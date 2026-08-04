using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Auth.Queries.LoginHistory
{
    public class GetLoginHistoryHandler : IRequestHandler<GetLoginHistoryQuery,PagedResult<LoginHistoryDto>>
    {
        private readonly ILoginHistoryRepository _loginRepository;
        public GetLoginHistoryHandler(ILoginHistoryRepository loginRepository) { 
            _loginRepository = loginRepository; 
        }

        public async Task<PagedResult<LoginHistoryDto>> Handle(GetLoginHistoryQuery query , CancellationToken token)
        {
            return await _loginRepository.GetLoginHistoriesFilteredAsync(query.UserId,query.Status,query.Search,query.From,query.To,query.Page,query.PageSize,token);
        }
    }
}
