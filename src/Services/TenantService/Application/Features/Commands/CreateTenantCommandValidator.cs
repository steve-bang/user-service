
// using FluentValidation;

// namespace Steve.ManagerHero.TenantService.Application.Features.Commands;

// public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
// {
//     public CreateTenantCommandValidator()
//     {
//         RuleFor(x => x.Name)
//             .NotEmpty()
//             .WithErrorCode(ErrorCodes.InputInvalid)
//             .WithMessage("Name is required.");

//         RuleFor(x => x.Subdomain)
//             .NotEmpty()
//             .WithErrorCode(ErrorCodes.InputInvalid)
//             .WithMessage("Subdomain is required.")
//             .Matches("^[a-zA-Z0-9-]+$")
//             .WithErrorCode(ErrorCodes.InputInvalid)
//             .WithMessage("Subdomain is invalid.");
//     }
// }
