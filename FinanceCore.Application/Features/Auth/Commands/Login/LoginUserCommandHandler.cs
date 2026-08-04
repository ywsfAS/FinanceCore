using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Auth;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.LoginHistory;
using FinanceCore.Domain.RefreshToken;
using MediatR;

namespace FinanceCore.Application.Features.Auth.Commands.Login;

public class LoginUserCommandHandler
    : IRequestHandler<LoginUserCommand, LoginDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ILoginHistoryRepository _loginHistoryRepository;
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly IPasswordHasher _hasher;
    private readonly IRefreshTokenHasher _refreshTokenHasher;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IRequestMetadata _requestMetadata;

    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IRefreshTokenHasher refreshTokenHasher,
        IRefreshTokenRepository tokenRepository,
        IPasswordHasher hasher,
        IJwtTokenGenerator jwtGenerator,
        IRefreshTokenGenerator generator,
        IRequestMetadata requestMetadata,
        ILoginHistoryRepository loginHistoryRepository)
    {
        _userRepository = userRepository;
        _hasher = hasher;
        _jwtGenerator = jwtGenerator;
        _refreshTokenGenerator = generator;
        _refreshTokenRepository = tokenRepository;
        _refreshTokenHasher = refreshTokenHasher;
        _loginHistoryRepository = loginHistoryRepository;
        _requestMetadata = requestMetadata;
    }

    public async Task<LoginDto> Handle(
        LoginUserCommand command,
        CancellationToken token = default)
    {
        var email = new Email(command.Email);

        var user = await _userRepository.GetByEmailAsync(email);

        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        if (user.IsLocked())
        {
            var history = LoginHistory.Create(
                userId: user.Id,
                ipAddress: _requestMetadata.IpAddress,
                userAgent: _requestMetadata.UserAgent,
                deviceName: _requestMetadata.DeviceName,
                os: _requestMetadata.Os,
                status: EnLoginStatus.LockedOut,
                failureReason: "Account is temporarily locked.");

            await _loginHistoryRepository.AddAsync(
                history,
                null,
                token);

            throw new UserAccountLockedException(
                user.Id,
                user.LockedUntil);
        }

        var isPasswordValid = _hasher.Verify(
            command.Password,
            user.PasswordHash);

        if (!isPasswordValid)
        {
            user.RecordFailedLoginAttempt(
                5,
                TimeSpan.FromMinutes(10));

            await _userRepository.UpdateLoginSecurityStateAsync(
                user.Id,
                user.FailedLoginAttempts,
                user.LockedUntil,
                token);

            var history = LoginHistory.Create(
                userId: user.Id,
                ipAddress: _requestMetadata.IpAddress,
                userAgent: _requestMetadata.UserAgent,
                deviceName: _requestMetadata.DeviceName,
                os: _requestMetadata.Os,
                status: EnLoginStatus.Failed,
                failureReason: "Invalid credentials.");

            await _loginHistoryRepository.AddAsync(
                history,
                null,
                token);

            throw new InvalidCredentialsException();
        }

        user.ResetLoginAttempts();

        await _userRepository.UpdateLoginSecurityStateAsync(
            user.Id,
            user.FailedLoginAttempts,
            user.LockedUntil,
            token);

        var jwtToken = _jwtGenerator.GenerateToken(user);

        var rawRefreshToken =
            _refreshTokenGenerator.GenerateRefreshToken();

        var refreshTokenHash =
            _refreshTokenHasher.Hash(rawRefreshToken);

        var expiresAt =
            DateTime.UtcNow.AddDays(7);

        var refreshToken = RefreshToken.Create(
            userId: user.Id,
            tokenHash: refreshTokenHash,
            expiresAt: expiresAt);

        await _refreshTokenRepository.AddAsync(
            refreshToken,
            token);

        var historySuccess = LoginHistory.Create(
            userId: user.Id,
            ipAddress: _requestMetadata.IpAddress,
            userAgent: _requestMetadata.UserAgent,
            deviceName: _requestMetadata.DeviceName,
            os: _requestMetadata.Os,
            status: EnLoginStatus.Success);

        await _loginHistoryRepository.AddAsync(
            historySuccess,
            null,
            token);

        return new LoginDto(
            user.Id,
            user.Email.Address,
            jwtToken,
            rawRefreshToken,
            expiresAt);
    }
}
