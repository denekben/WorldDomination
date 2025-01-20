using Game.Application.Services;
using Game.Domain.DomainModels.Messaging.Entities;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Application.UseCases.Messages.Negotiations.Commands.Handlers
{
    internal sealed class SendNegotiationRequestHandler : IRequestHandler<SendNegotiationRequest>
    {
        private readonly IGameModuleReadService _readService;
        private readonly INegotiationRequestRepository _requestRepository;
        private readonly ILogger<SendNegotiationRequestHandler> _logger;
        private readonly IGameModuleNotificationService _notifications;

        public SendNegotiationRequestHandler(IGameModuleReadService readService, ILogger<SendNegotiationRequestHandler> logger,
            INegotiationRequestRepository requestRepository, IGameModuleNotificationService notifications)
        {
            _readService = readService;
            _logger = logger;
            _requestRepository = requestRepository;
            _notifications = notifications;
        }

        public async Task Handle(SendNegotiationRequest command, CancellationToken cancellationToken)
        {
            var member = await _readService.GetRoomMemberAsync(command.CallerId, command.RoomId)
                ?? throw new BadRequestException($"Cannot find RoomMember {command.CallerId}");

            if (member.CountryId == null)
                throw new BadRequestException($"Member does not balong any country");

            if(!await _readService.CountryExistsByIdAsync(command.AudienceCountryId))
                throw new BadRequestException($"Audience country {command.AudienceCountryId} does not exists");

            if (await _readService.NegotationChannelExists((Guid)member.CountryId, command.AudienceCountryId))
                throw new BadRequestException($"Cannot duplicate channels");

            var request = NegotiationRequest.Create((Guid) member.CountryId, command.AudienceCountryId, command.CallerId)
                ?? throw new BadImageFormatException($"Cannot create NegotiationRequest from {(Guid)member.CountryId} to {command.AudienceCountryId}");

            try
            {
                await _requestRepository.AddAsync(request);
            }
            catch
            {
                throw new BadRequestException("Cannot duplicate requests");
            }

            _logger.LogInformation($"Request from {(Guid)member.CountryId} to {command.AudienceCountryId} by {command.CallerId} created");

            await _notifications.NegotiationRequestSent((Guid) member.CountryId, command.AudienceCountryId);
        }
    }
}
