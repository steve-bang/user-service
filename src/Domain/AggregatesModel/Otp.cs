
/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/


using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Domain.AggregatesModel;

public class Otp : AggregateRoot
{
    public Guid? UserId { get; private set; }

    public User? User { get; private set; }

    public string PhoneNumber { get; private set; }

    public OtpType Type { get; private set; }

    public string OtpHash { get; private set; }

    public string Salt { get; private set; }

    public DateTime ExpirationTime { get; private set; }

    public DateTime? ConsumedAt { get; private set; }

    public bool IsUsed { get; private set; }

    public int RetryCount { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public bool IsExpired => DateTime.UtcNow > ExpirationTime;

    private Otp() : base() { }

    public Otp(Guid? userId, string phoneNumber, OtpType type, string otpHash, string salt, int ttlMinutes) : this()
    {
        UserId = userId;
        PhoneNumber = phoneNumber;
        Type = type;
        OtpHash = otpHash;
        Salt = salt;
        ExpirationTime = DateTime.UtcNow.AddMinutes(ttlMinutes);
        IsUsed = false;
        RetryCount = 0;
        CreatedAt = DateTime.UtcNow;
    }

    public void Consume()
    {
        IsUsed = true;
        ConsumedAt = DateTime.UtcNow;
    }

    public void IncrementRetry()
    {
        RetryCount++;
    }

}