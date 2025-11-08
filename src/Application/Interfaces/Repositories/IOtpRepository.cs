
/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.Interfaces.Repository;

public interface IOtpRepository : IRepository
{
    Task<Otp> AddAsync(Otp otp, CancellationToken ct = default);

    Task<Otp?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken ct = default);

    Task<List<Otp>> GetOtpNotUsedByUserId(Guid userId, CancellationToken ct = default);

    void Remove(Otp otp);

    void RemoveRange(List<Otp> otpList);
}