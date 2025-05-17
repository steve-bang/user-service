/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Infrastructure.Repositories;

public class SocialProviderRepository(
    UserAppContext _context
) : ISocialProviderRepository
{

    public async Task<SocialProvider?> GetByIdAsync(Guid id)
    {
        return await _context.SocialProviders
            .Include(p => p.SocialUsers)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<SocialProvider?> GetByNameAsync(string name)
    {
        return await _context.SocialProviders
            .Include(p => p.SocialUsers)
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<SocialProvider?> GetByTypeAsync(SocialProviderType type)
    {
        return await _context.SocialProviders
            .Include(p => p.SocialUsers)
            .FirstOrDefaultAsync(p => p.Type == type);
    }

    public IQueryable<SocialProvider> GetAll()
    {
        return _context.SocialProviders
            .Include(p => p.SocialUsers)
            .AsQueryable();
    }

    public async Task AddAsync(SocialProvider provider)
    {
        await _context.SocialProviders.AddAsync(provider);
    }

    public Task UpdateAsync(SocialProvider provider)
    {
        _context.SocialProviders.Update(provider);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(SocialProvider provider)
    {
        _context.SocialProviders.Remove(provider);
        return Task.CompletedTask;
    }
}