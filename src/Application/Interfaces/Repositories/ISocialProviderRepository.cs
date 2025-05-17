/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Application.Interfaces.Repository;

public interface ISocialProviderRepository : IRepository
{
    Task<SocialProvider?> GetByIdAsync(Guid id);
    Task<SocialProvider?> GetByNameAsync(string name);
    Task<SocialProvider?> GetByTypeAsync(SocialProviderType type);
    IQueryable<SocialProvider> GetAll();
    Task AddAsync(SocialProvider provider);
    Task UpdateAsync(SocialProvider provider);
    Task DeleteAsync(SocialProvider provider);
} 