
// using Steve.ManagerHero.BuildingBlocks.CQRS;
// using Steve.ManagerHero.TenantService.Domain.AggregatesModel;
// using Steve.ManagerHero.TenantService.Domain.Exception;

// namespace Steve.ManagerHero.TenantService.Application.Features.Commands;

// public record CreateTenantCommandHandler(
//     IUnitOfWork unitOfWork
// ) : ICommandHandler<CreateTenantCommand, Guid>
// {
//     private readonly string Domain = "example.com";

//     public async Task<Guid> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
//     {
//         // Build domain
//         string domain = $"{request.Subdomain}.{Domain}";

//         // Check exists domain
//         if (await unitOfWork.Tenants.IsExistsDomainAsync(domain, cancellationToken))
//             throw new DomainExistsException();
        

//         Tenant tenant = new(
//             name: request.Name,
//             description: request.Description,
//             domain: domain
//         );

//         await unitOfWork.Tenants.AddAsync(tenant, cancellationToken);
//         await unitOfWork.SaveChangesAsync(cancellationToken);

//         return tenant.Id;
//     }
// }