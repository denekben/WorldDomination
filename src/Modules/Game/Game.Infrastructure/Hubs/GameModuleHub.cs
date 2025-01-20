using Game.Application.DTOs;
using Game.Application.Services;
using Game.Application.UseCases.Countries;
using Game.Application.UseCases.Rooms.Commands;
using Game.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Hubs
{
    [Authorize]
    public sealed class GameModuleHub : Hub
    {
        private readonly ISender _sender;
        private readonly IGameModuleNotificationService _notifications;
        private readonly GameReadDbContext _dbContext;

        public GameModuleHub(ISender sender, IGameModuleNotificationService notifications)
        {
            _sender = sender;
            _notifications = notifications;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = new Guid(Context.UserIdentifier);
            var roomMember = await _dbContext.RoomMembers.FirstOrDefaultAsync(m => m.GameUserId == userId);
            var roomId = roomMember?.RoomId.ToString();
            var countryId = roomMember?.CountryId.ToString();

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            if (countryId != null)
                await Groups.AddToGroupAsync(Context.ConnectionId, countryId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        // Rooms
        public async Task JoinRoom(Guid roomId, string? roomCode)
        {
            var command = new JoinRoom(new Guid(Context.ConnectionId), roomId, roomCode);
            var roomDto = await _sender.Send(command);

            await Groups.AddToGroupAsync(Context.ConnectionId, command.RoomId.ToString());

            await Clients.Caller.SendAsync(nameof(JoinRoom), roomDto);
        }

        public async Task LeaveRoom(Guid roomId, Guid? previousCountryId = null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
            if(previousCountryId != null)
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, previousCountryId.ToString());

            var command = new LeaveRoom(new Guid(Context.ConnectionId), roomId);
            await _sender.Send(command);
        }

        public async Task CreateRoom(string? roomName, string gameType, bool hasTeams,
        int memberLimit, int roundQuantity, int countryLimit, bool isPrivate, string? roomCode)
        {
            var command = new CreateRoom(new Guid(Context.ConnectionId), roomName, gameType, 
                hasTeams, memberLimit, roundQuantity, countryLimit, isPrivate, roomCode);
            var roomId = await _sender.Send(command);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());

            await Clients.Caller.SendAsync(nameof(CreateRoom), roomId);
        }

        public async Task KickMember()
        {

        }

        // Countries
        public async Task CreateCountry(string normalizedName, Guid roomId, Guid? previousCountryId = null)
        {
            if (previousCountryId != null)
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, previousCountryId.ToString());

            var command = new CreateCountry(new Guid(Context.ConnectionId), normalizedName, roomId);
            var countryId = await _sender.Send(command);

            await Groups.AddToGroupAsync(Context.ConnectionId, countryId.ToString());

            await Clients.Caller.SendAsync(nameof(CreateCountry), countryId);
        }

        public async Task JoinCountry(Guid countryId, Guid roomId, Guid? previousCountryId = null)
        {
            if (previousCountryId != null)
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, previousCountryId.ToString());

            var command = new JoinCountry(new Guid(Context.ConnectionId), countryId, roomId);
            var countryDto = await _sender.Send(command);

            await Groups.AddToGroupAsync(Context.ConnectionId, countryDto.Id.ToString());

            await Clients.Caller.SendAsync(nameof(JoinCountry), countryDto);
        }

        // Game
        public async Task ChangeOrder(OrderDto orderDto)
        {
            await _notifications.OrderChanged(orderDto, Context.ConnectionId.ToString());
        }
    }
}
