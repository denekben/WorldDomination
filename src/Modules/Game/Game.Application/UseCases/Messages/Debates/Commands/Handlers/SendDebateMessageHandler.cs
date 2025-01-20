using Game.Application.DTOs.Mappers;
using Game.Application.Services;
using Game.Domain.DomainModels.Messaging.Entities;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Application.UseCases.Messages.Debates.Commands.Handlers
{
    internal sealed class SendDebateMessageHandler : IRequestHandler<SendDebateMessage>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IGameModuleNotificationService _notifications;
        private readonly IGameModuleReadService _readService;
        private readonly ILogger<SendDebateMessageHandler> _logger;

        public SendDebateMessageHandler(IChatRepository chatRepository,
            IGameModuleNotificationService notifications, IGameModuleReadService readService, 
            ILogger<SendDebateMessageHandler> logger)
        {
            _chatRepository = chatRepository;
            _notifications = notifications;
            _readService = readService;
            _logger = logger;
        }

        public async Task Handle(SendDebateMessage command, CancellationToken cancellationToken)
        {
            var member = await _readService.GetRoomMemberAsync(command.CallerId, command.RoomId)
                ?? throw new BadRequestException($"Cannot find RoomMember {command.CallerId}");

            var message = Message.Create(command.CallerId, command.RoomId, command.MessageText);

            await _chatRepository.AddAsync(message);
            await _notifications.MessageSent(member.AsRoomMemberDto(), command.MessageText, command.RoomId);

            _logger.LogInformation($"Member {member.GameUserId} sent message to chat {command.RoomId}");
        }
    }
}
