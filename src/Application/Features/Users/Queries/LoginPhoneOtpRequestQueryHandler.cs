
using Microsoft.Extensions.Options;
using Steve.ManagerHero.BuildingBlocks.CQRS;
using Steve.ManagerHero.BuildingBlocks.Helpers;
using Steve.ManagerHero.BuildingBlocks.Policy.Otp;
using Steve.ManagerHero.BuildingBlocks.SMS;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class LoginPhoneOtpRequestQueryHandler(
    IUnitOfWork _unitOfWork,
    ISmsSender _smsSender,
    IPasswordHasher _passwordHash,
    IOptions<OtpPolicyOptions> _otpPolicyOptions
) : IQueryHandler<LoginPhoneOtpRequestQuery, string>
{
    private readonly OtpPolicyOptions OtpPolicyOptions = _otpPolicyOptions.Value;

    public async Task<string> Handle(LoginPhoneOtpRequestQuery request, CancellationToken cancellationToken)
    {
        User user = await _unitOfWork.Users.GetByPhoneNumberAsync(request.PhoneNumber, cancellationToken)
                        ?? throw new UserNotFoundException();

        List<Otp> otpList = await _unitOfWork.Otps.GetOtpNotUsedByUserId(user.Id, cancellationToken);
        _unitOfWork.Otps.RemoveRange(otpList);

        var otpCode = StringHelper.GenerateNumericOtp(OtpPolicyOptions.MaxLength);

        (string hash, string salt) = _passwordHash.Hash(otpCode);

        var otp = new Otp(
            userId: user.Id,
            phoneNumber: request.PhoneNumber,
            type: UserService.Domain.Constants.OtpType.LoginPhone,
            otpHash: hash,
            salt: salt,
            ttlMinutes: OtpPolicyOptions.TtlMinutes
        );

        await _unitOfWork.Otps.AddAsync(otp, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        string message = $"Your [Steve] verification code is: {otpCode}. Don't share this code with anyone.";

        _ = _smsSender.SendAsync(
            new BuildingBlocks.SMS.Options.SmsRecipientOptions(
                request.PhoneNumber,
                message
            )
        );

        return "OTP has been sent to your phone.";
    }
}