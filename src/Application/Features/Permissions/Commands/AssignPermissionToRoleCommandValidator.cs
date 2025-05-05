/*
* Author: Steve Bang
* History:
* - [2025-04-05] - Created by mrsteve.bang@gmail.com
*/

using FluentValidation;

namespace Steve.ManagerHero.Application.Features.Permissions.Commands;

public class AssignPermissionToRoleCommandValidator : AbstractValidator<AssignPermissionToRoleCommand>
{
    public AssignPermissionToRoleCommandValidator()
    {

        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("The id of the role is required.");

        RuleFor(x => x.PermissionIds)
            .NotEmpty()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("At least one permission is required.");

    }
}