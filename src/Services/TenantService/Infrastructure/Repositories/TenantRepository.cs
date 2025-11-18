
// using Microsoft.EntityFrameworkCore;
// using Steve.ManagerHero.TenantService.Application.Interfaces.Repositories;
// using Steve.ManagerHero.TenantService.Domain.AggregatesModel;
// using Steve.ManagerHero.UserService.Infrastructure;

// namespace Steve.ManagerHero.TenantService.Infrastructure.Repositories;

// public class TenantRepository(
//     UserAppContext _context
// ) : ITenantRepository
// {
//     public async Task<Tenant> AddAsync(Tenant tenant, CancellationToken cancellationToken = default)
//     {
//         var tenantAdded = await _context.Tenants.AddAsync(tenant, cancellationToken);

//         return tenantAdded.Entity;
//     }
//     public Task<Tenant?> GetByDomainAsync(string domain, CancellationToken cancellationToken = default)
//     {
//         return _context.Tenants.FirstOrDefaultAsync(t => t.Domain == domain, cancellationToken);
//     }

//     public Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
//     {
//         return _context.Tenants.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
//     }

//     public Task<bool> IsExistsDomainAsync(string domain, CancellationToken cancellationToken = default)
//     {
//         return _context.Tenants.AnyAsync(t => t.Domain == domain, cancellationToken);
//     }

//     public Tenant Update(Tenant tenant)
//     {
//         var result = _context.Tenants.Update(tenant);

//         return result.Entity;
//     }
// }
