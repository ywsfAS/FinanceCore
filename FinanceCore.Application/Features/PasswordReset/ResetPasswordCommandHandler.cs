using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinanceCore.Domain.Exceptions.ResetPasswordException;

namespace FinanceCore.Application.Features.PasswordReset
{
    public class ResetPasswordCommandHandler
    : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IPasswordResetTokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ResetPasswordCommandHandler(
            IPasswordResetTokenRepository tokenRepository,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(
            ResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var resetToken = await _tokenRepository.GetByTokenAsync(request.Token,cancellationToken);
            if(resetToken is null || resetToken.IsExpired())
            {
                throw new InvalidTokenException();
            }
            if (resetToken.IsUsed) { 
                throw new UsedTokenException();
            }
            var user = await _userRepository.GetByIdAsync(
            resetToken.UserId,
            cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException(resetToken.UserId);
            }
            var hashedPassword = _passwordHasher.Hash(request.NewPassword);
            user.ChangePassword(hashedPassword);
            await _userRepository.UpdateAsync(user);
            // Mark token as Used
            resetToken.MarkAsUsed();
            await _tokenRepository.MarkAsUsedAsync(resetToken,cancellationToken);
        }
    }
}
