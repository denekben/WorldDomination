using Game.Shared.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace Game.Infrastructure.Realtime
{
    public sealed class GameModuleHub : Hub<IGameModuleHubClient>
    {
        public async Task JoinRoomGroup(Guid roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        }

        public async Task JoinCountryGroup(Guid countryId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, countryId.ToString()); 
        }

        public async Task ChangeOrder(OrderDto orderDto)
        {
            await Clients.Group(orderDto.CountryId.ToString()).OrderChanged(orderDto);
        }
    } 
}
