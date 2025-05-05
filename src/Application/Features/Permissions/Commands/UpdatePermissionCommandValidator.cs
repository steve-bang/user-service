/*
* Author: Steve Bang
* History:
* - [2025-04-05] - Created by mrsteve.bang@gmail.com
*/

using FluentValidation;

namespace Steve.ManagerHero.Application.Features.Permissions.Commands;

public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
{
    public UpdatePermissionCommandValidator()
    {

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Code is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Name is required.");

    }
}