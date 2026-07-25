using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.PasswordRestToken;
using MediatR;
using Microsoft.Extensions.Logging;
namespace FinanceCore.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler
    : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _tokenRepository;
        private readonly IEmailService _emailService;
        private readonly IFrontendSettingsProvider _frontendSettingsProvider;
        private readonly ILogger<ForgotPasswordCommandHandler> _logger;

        public ForgotPasswordCommandHandler(
            IFrontendSettingsProvider settings,
            IUserRepository userRepository,
            ILogger<ForgotPasswordCommandHandler> logger,
            IPasswordResetTokenRepository tokenRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _emailService = emailService;
            _frontendSettingsProvider = settings;
            _logger = logger;
        }

        public async Task Handle(ForgotPasswordCommand request,CancellationToken cancellationToken)
        {
            var email = new Email(request.Email);
            var user = await _userRepository.GetByEmailAsync(email,cancellationToken);
            if(user is null)
            {
                throw new InvalidCredentialsException();
            }
            var token = Guid.NewGuid().ToString("N");
            var expiredAt = DateTime.UtcNow.AddMinutes(15);
            var resetToken = new PasswordResetToken(user.Id,token,expiredAt);
            var resetLink = $"{_frontendSettingsProvider.FrontendBaseUrl}/reset-password?token={token}";
            try
            {
                await _emailService.SendEmailAsync(email, "Reset your password",resetLink);
                await _tokenRepository.AddAsync(resetToken,cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex,"Failed to send reset password email to {email}",request.Email);
                throw;
            }
        }
    }
};
