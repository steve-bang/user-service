
namespace Steve.ManagerHero.BuildingBlocks.Policy.Otp;

public class OtpPolicyOptions
{
    public int MaxLength { get; init; }

    public int TtlMinutes { get; init; }

    public int MaxRetry { get; init; }
}