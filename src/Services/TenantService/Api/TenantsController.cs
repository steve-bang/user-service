
// using Microsoft.AspNetCore.Mvc;
// using Steve.ManagerHero.Api.Models;
// using Steve.ManagerHero.TenantService.Application.DTOs;
// using Steve.ManagerHero.TenantService.Application.Features.Commands;

// [Route("api/v1/tenants")]
// public class TenantsController : ControllerBase
// {
//     private readonly IMediator _mediator;

//     public TenantsController(
//         IMediator mediator
//     )
//     {
//         _mediator = mediator;
//     }

//     [HttpPost]
//     public async Task<IActionResult> CreateTenant([FromBody] TenantCreateRequestDto createRequestDto)
//     {
//         var command = new CreateTenantCommand(
//             createRequestDto.Subdomain,
//             createRequestDto.Name,
//             createRequestDto.Description
//         );

//         var result = await _mediator.Send(command);

//         return ApiResponseSuccess<Guid>.BuildCreatedObjectResult(result);
//     }
// }