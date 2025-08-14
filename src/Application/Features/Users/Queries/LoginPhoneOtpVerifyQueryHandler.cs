
using Microsoft.Extensions.Options;
using Steve.ManagerHero.BuildingBlocks.CQRS;
using Steve.ManagerHero.BuildingBlocks.Policy.Otp;
using Steve.ManagerHero.BuildingBlocks.Security.Jwt;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class LoginPhoneOtpVerifyQueryHandler(
    IUnitOfWork _unitOfWork,
    IJwtHandler _jwtHandler,
    IPasswordHasher _passwordHasher,
    IOptions<OtpPolicyOptions> _otpPolicyOptions,
    IHttpContextAccessor _httpContextAccessor
) : IQueryHandler<LoginPhoneOtpVerifyQuery, AuthenticationResponseDto>
{
    private readonly OtpPolicyOptions OtpPolicyOptions = _otpPolicyOptions.Value;

    public async Task<AuthenticationResponseDto> Handle(LoginPhoneOtpVerifyQuery request, CancellationToken cancellationToken)
    {
        var otp = await _unitOfWork.Otps.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken)
                  ?? throw new OtpInvalidException();

        ValidateOtp(otp);

        if (!_passwordHasher.Verify(request.OtpCode, otp.OtpHash, otp.Salt))
        {
            HandleInvalidAttempt(otp);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            throw new OtpInvalidException();
        }

        var user = otp.User ?? throw new UserNotFoundException();

        // Login with passwordless method
        user.LoginPassword();

        // Consume otp
        otp.Consume();

        // Generate access token and session
        var session = new Session(user);

        // Generate token
        (string accessToken, string refreshToken, DateTime expires) = _jwtHandler.GenerateToken(user.Id, session.Id);

        if (_httpContextAccessor.HttpContext != null)
        {
            session.Update(refreshToken, _httpContextAccessor.HttpContext, expires);
        }

        await _unitOfWork.Sessions.CreateAsync(session, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthenticationResponseDto(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresIn: expires
        );
    }

    private static void ValidateOtp(Otp otp)
    {
        if (otp.IsExpired)
            throw new OtpExpiredException();

        if (otp.IsUsed)
            throw new OtpUsedException();
    }

    private void HandleInvalidAttempt(Otp otp)
    {
        if (otp.RetryCount > OtpPolicyOptions.MaxLength)
            throw new OtpDeniedException();

        otp.IncrementRetry();
    }
}