/*
* Author: Steve Bang
* History:
* - [2025-08-09] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;

namespace Steve.ManagerHero.UserService.Infrastructure.Repository;

public class OtpRepository(UserAppContext _context) : IOtpRepository
{
    public async Task<Otp> AddAsync(Otp log, CancellationToken ct = default)
    {
        var otpAdded = await _context.Otps.AddAsync(log, ct);

        return otpAdded.Entity;
    }

    public Task<Otp?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken ct = default)
    {
        return _context.Otps
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber, ct);
    }

    public Task<List<Otp>> GetOtpNotUsedByUserId(Guid userId, CancellationToken ct = default)
    {
        return _context.Otps
            .Where(x => x.UserId == userId && x.IsUsed == false)
            .ToListAsync(ct);
    }

    public void Remove(Otp otp)
    {
        if (otp != null)
            _context.Otps.Remove(otp);
    }

    public void RemoveRange(List<Otp> otpList)
    {
        if (otpList != null && otpList.Count > 0)
            _context.Otps.RemoveRange(otpList);
    }
}