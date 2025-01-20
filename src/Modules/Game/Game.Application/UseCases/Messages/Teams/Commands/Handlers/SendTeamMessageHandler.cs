using Game.Application.DTOs.Mappers;
using Game.Application.Services;
using Game.Application.UseCases.Messages.Teams.Commands;
using Game.Domain.DomainModels.Messaging.Entities;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Application.UseCases.Messages.Debates.Commands.Handlers
{
    internal sealed class SendTeamMessageHandler : IRequestHandler<SendTeamMessage>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IGameModuleNotificationService _notifications;
        private readonly IGameModuleReadService _readService;
        private readonly ILogger<SendTeamMessageHandler> _logger;

        public SendTeamMessageHandler(IGameModuleReadService readService,
            IChatRepository chatRepository, IGameModuleNotificationService notifications, 
            ILogger<SendTeamMessageHandler> logger)
        {
            _readService = readService;
            _chatRepository = chatRepository;
            _notifications = notifications;
            _logger = logger;
        }

        public async Task Handle(SendTeamMessage command, CancellationToken cancellationToken)
        {
            var member = await _readService.GetRoomMemberAsync(command.CallerId, command.RoomId)
                ?? throw new BadRequestException($"Cannot find RoomMember {command.CallerId}");
            if(member.CountryId == null)
                throw new BadRequestException($"RoomMember {command.CallerId} does not belong any Country");
            var message = Message.Create(command.CallerId,(Guid) member.CountryId, command.MessageText);

            await _chatRepository.AddAsync(message);
            await _notifications.MessageSent(member.AsRoomMemberDto(), command.MessageText, (Guid) member.CountryId);

            _logger.LogInformation($"Member {member.GameUserId} sent message to chat {command.RoomId}");
        }
    }
}
