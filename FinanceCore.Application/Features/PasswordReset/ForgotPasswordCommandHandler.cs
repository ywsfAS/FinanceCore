using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.PasswordRestToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.PasswordReset
{
    public class ForgotPasswordCommandHandler
    : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _tokenRepository;
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(
            IUserRepository userRepository,
            IPasswordResetTokenRepository tokenRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _emailService = emailService;
        }

        public async Task Handle(ForgotPasswordCommand request,CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email.Address,cancellationToken);
            if(user is not null)
            {
                var token = Guid.NewGuid().ToString("N");
                var expiredAt = DateTime.UtcNow.AddMinutes(15);
                var resetToken = new PasswordResetToken(user.Id,token,expiredAt);
                await _tokenRepository.AddAsync(resetToken,cancellationToken);
                var resetLink = $"http://localhost:5173/reset-password?token={token}";
                await _emailService.SendEmailAsync(request.Email, "Reset your password",resetLink);
            }
            return;
        }
    }
};
