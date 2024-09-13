using Shared.Events;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shared.Messaging
{
    internal sealed class EventDispatcherJob : BackgroundService
    {
        private readonly IEventChannel _eventChannel;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<EventDispatcherJob> _logger;

        public EventDispatcherJob(IEventChannel eventChannel, IEventDispatcher eventDispatcher,
            ILogger<EventDispatcherJob> logger)
        {
            _eventChannel = eventChannel;
            _eventDispatcher = eventDispatcher;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var @event in _eventChannel.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await _eventDispatcher.PublishAsync(@event, stoppingToken);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, exception.Message);
                }
            }
        }
    }
}
