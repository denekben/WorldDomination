using Game.Application.DTOs.Mappers;
using Game.Application.Services;
using Game.Domain.DomainModels.Messaging.Entities;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Application.UseCases.Messages.Negotiations.Commands.Handlers
{
    internal sealed class SendNegotiationMessageHandler : IRequestHandler<SendNegotiationMessage>
    {
        private readonly IGameModuleReadService _readService;
        private readonly INegotiationChatRepository _chatRepository;
        private readonly ILogger<SendNegotiationMessageHandler> _logger;
        private readonly IGameModuleNotificationService _notifications;

        public SendNegotiationMessageHandler(IGameModuleReadService readService, ILogger<SendNegotiationMessageHandler> logger,
            INegotiationChatRepository chatRepository, IGameModuleNotificationService notifications)
        {
            _readService = readService;
            _logger = logger;
            _chatRepository = chatRepository;
            _notifications = notifications;
        }

        public async Task Handle(SendNegotiationMessage command, CancellationToken cancellationToken)
        {
            var member = await _readService.GetRoomMemberAsync(command.CallerId, command.RoomId)
                ?? throw new BadRequestException($"Cannot find RoomMember {command.CallerId}");

            if (member.CountryId == null)
                throw new BadRequestException($"Member does not belong any country");

            if (!await _readService.CountryExistsByIdAsync(command.AudienceCountryId))
                throw new BadRequestException($"Audience country {command.AudienceCountryId} does not exists");

            var request = await _readService.GetNegotiationRequestByChannelAsync((Guid)member.CountryId, command.AudienceCountryId)
                ?? throw new BadRequestException($"Cannot send messages without applied channel");

            if (member.CountryId != request.AudienceCountryId && member.GameUserId != request.IssuerMemberId)
                throw new BusinessRuleValidationException("Only audience country members or issuer can send messages in negotiation chat");

            var chat = await _chatRepository.GetAsync(request.IssuerCountryId, request.AudienceCountryId);

            chat ??= NegotiationChat.Create(request.IssuerCountryId, request.AudienceCountryId);

            var message = Message.Create(member.GameUserId, chat.Id, command.MessageText);
            chat.AddMessage(message);
            await _chatRepository.UpdateAsync(chat);

            await _notifications.MessageSent(member.AsRoomMemberDto(), command.MessageText, chat.Id);
            _logger.LogInformation($"Member {member.GameUserId} sent message to chat {chat.Id}");
        }
    }
}
