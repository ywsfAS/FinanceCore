using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.PasswordRestToken;
using FinanceCore.Domain.Common;
namespace FinanceCore.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler
    : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _tokenRepository;
        private readonly IEmailService _emailService;
        private readonly IFrontendSettingsProvider _frontendSettingsProvider;

        public ForgotPasswordCommandHandler(
            IFrontendSettingsProvider settings,
            IUserRepository userRepository,
            IPasswordResetTokenRepository tokenRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _emailService = emailService;
            _frontendSettingsProvider = settings;
        }

        public async Task Handle(ForgotPasswordCommand request,CancellationToken cancellationToken)
        {
            var email = new Email(request.Email);
            var user = await _userRepository.GetByEmailAsync(email,cancellationToken);
            if(user is not null)
            {
                var token = Guid.NewGuid().ToString("N");
                var expiredAt = DateTime.UtcNow.AddMinutes(15);
                var resetToken = new PasswordResetToken(user.Id,token,expiredAt);
                await _tokenRepository.AddAsync(resetToken,cancellationToken);
                var resetLink = $"{_frontendSettingsProvider.FrontendBaseUrl}/reset-password?token={token}";
                await _emailService.SendEmailAsync(email, "Reset your password",resetLink);
            }
            return;
        }
    }
};
