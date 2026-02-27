using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using MediatR;
using FinanceCore.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Transactions;

namespace FinanceCore.Application.Features.Transactions.Commands.Income
{
    public class IncomeHandler : IRequestHandler<IncomeCommand, IncomeDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        public IncomeHandler(ITransactionRepository transactionRepository,IAccountRepository accountRepository , ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IncomeDto> Handle(IncomeCommand command, CancellationToken token) {
            var account = await _accountRepository.GetByIdAsync(command.AccountId , token);
            if (account == null) { 
                throw new AccountNotFoundException(command.AccountId);
            }
            var category = await _categoryRepository.GetByIdAsync(command.CategoryId,token);
            if ( category == null)
            {
                throw new CategoryNotFoundException(command.CategoryId);
            }
            var transaction = Transaction.Create(command.AccountId,null , command.Amount , command.CategoryId , EnTransactionType.Income , DateTime.UtcNow , command.Description);

            return await _transactionRepository.IncomeAsync(transaction, token);
        }
        
    


} 
}
