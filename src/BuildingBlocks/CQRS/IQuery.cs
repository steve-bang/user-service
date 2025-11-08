
namespace Steve.ManagerHero.BuildingBlocks.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>;