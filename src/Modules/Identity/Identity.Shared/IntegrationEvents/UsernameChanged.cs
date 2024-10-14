using Shared.Events;
using System;

namespace Identity.Shared.IntegrationEvents
{
    public sealed record UsernameChanged(string UserId, string Username) : IEvent;
}
