using Microsoft.AspNetCore.SignalR;

namespace Game.Infrastructure.Realtime
{
    public sealed class GameModuleHub : Hub<IGameModuleHubClient>
    {

    }
}
