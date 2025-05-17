/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

using MediatR;
using Steve.ManagerHero.UserService.Domain.AggregatesModel;

namespace Steve.ManagerHero.UserService.Application.Features.SocialProviders.Commands;

public record DeleteSocialProviderCommand(Guid Id) : IRequest;

public class DeleteSocialProviderCommandHandler(
    IUnitOfWork _unitOfWork
) : IRequestHandler<DeleteSocialProviderCommand>
{

    public async Task Handle(DeleteSocialProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = await _unitOfWork.SocialProviders.GetByIdAsync(request.Id);
        if (provider == null)
            throw new KeyNotFoundException($"Social provider with ID {request.Id} not found");

        await _unitOfWork.SocialProviders.DeleteAsync(provider);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
} 