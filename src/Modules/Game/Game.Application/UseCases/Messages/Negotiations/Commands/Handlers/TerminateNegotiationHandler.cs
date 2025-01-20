using Game.Application.Services;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Application.UseCases.Messages.Negotiations.Commands.Handlers
{
    internal sealed class TerminateNegotiationHandler : IRequestHandler<TerminateNegotiation>
    {
        private readonly IGameModuleReadService _readService;
        private readonly INegotiationRequestRepository _requestRepository;
        private readonly ILogger<TerminateNegotiationHandler> _logger;
        private readonly IGameModuleNotificationService _notifications;
        private readonly INegotiationChatRepository _chatRepository;

        public TerminateNegotiationHandler(IGameModuleReadService readService, ILogger<TerminateNegotiationHandler> logger,
            INegotiationRequestRepository requestRepository, IGameModuleNotificationService notifications,
            INegotiationChatRepository chatRepository)
        {
            _readService = readService;
            _logger = logger;
            _requestRepository = requestRepository;
            _notifications = notifications;
            _chatRepository = chatRepository;
        }

        public async Task Handle(TerminateNegotiation command, CancellationToken cancellationToken)
        {
            var member = await _readService.GetRoomMemberAsync(command.CallerId, command.RoomId)
                ?? throw new BadRequestException($"Cannot find RoomMember {command.CallerId}");

            var chat = await _chatRepository.GetAsync(command.ChatId)
                ?? throw new BadRequestException($"Cannot find Chat {command.ChatId}");

            var request = await _requestRepository.GetAsync(chat.FirstCountryId, chat.SecondCountryId)
                ?? throw new BadRequestException($"Cannot find {chat.FirstCountryId} - {chat.SecondCountryId} channel");

            if (member.CountryId == null)
                throw new BadRequestException($"Member does not belong any country");

            if (member.GameUserId != request.IssuerMemberId && (Guid) member.CountryId != request.AudienceCountryId)
                throw new BusinessRuleValidationException($"Only request issuer or audience country members can terminate negotiation");

            await _requestRepository.DeleteAsync(request);
            await _notifications.NegotiationTerminated(chat.FirstCountryId, chat.SecondCountryId);
            _logger.LogInformation($"Negotiation {chat.FirstCountryId} - {chat.SecondCountryId} terminated");
        }
    }
}
