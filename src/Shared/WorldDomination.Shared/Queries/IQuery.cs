using MediatR;

namespace WorldDomination.Shared.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
