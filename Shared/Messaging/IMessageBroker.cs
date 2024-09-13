using Shared.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Messaging
{
    public interface IMessageBroker
    {
        Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default);
    }
}
