
namespace Steve.ManagerHero.BuildingBlocks.CQRS;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : notnull;