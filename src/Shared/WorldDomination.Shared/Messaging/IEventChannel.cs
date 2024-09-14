using Shared.Events;
using System.Threading.Channels;

namespace Shared.Messaging
{
    internal interface IEventChannel
    {
        ChannelReader<IEvent> Reader { get; }
        ChannelWriter<IEvent> Writer { get; }
    }
}
