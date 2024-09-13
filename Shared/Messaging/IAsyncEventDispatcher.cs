using Shared.Events;
using System.Threading.Tasks;
using System.Threading;

namespace Shared.Messaging
{
    internal interface IAsyncEventDispatcher
    {
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : class, IEvent;
    }
}
