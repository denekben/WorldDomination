using Game.Application.Services;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Application.UseCases.Messages.Negotiations.Commands.Handlers
{
    internal sealed class ApplyNegotiationRequestHandler : IRequestHandler<ApplyNegotiationRequest>
    {
        private readonly IGameModuleReadService _readService;
        private readonly INegotiationRequestRepository _negotiationRepository;
        private readonly ILogger<ApplyNegotiationRequestHandler> _logger;
        private readonly IGameModuleNotificationService _notifications;

        public ApplyNegotiationRequestHandler(IGameModuleReadService readService, INegotiationRequestRepository negotiationRepository,
            ILogger<ApplyNegotiationRequestHandler> logger, IGameModuleNotificationService notifications)
        {
            _readService = readService;
            _negotiationRepository = negotiationRepository;
            _logger = logger;
            _notifications = notifications;
        }

        public async Task Handle(ApplyNegotiationRequest command, CancellationToken cancellationToken)
        {
            var member = await _readService.GetRoomMemberAsync(command.CallerId, command.RoomId)
                ?? throw new BadRequestException($"Cannot find RoomMember {command.CallerId}");

            if (member.CountryId == null)
                throw new BadRequestException($"Member does not balong any country");

            if (!await _readService.CountryExistsByIdAsync(command.IssuerCountryId))
                throw new BadRequestException($"Issuer country {command.IssuerCountryId} does not exists");

            var request = await _negotiationRepository.GetAsync(command.IssuerCountryId, (Guid)member.CountryId)
                ?? throw new BadRequestException($"Request does not exists");

            if ((Guid) member.CountryId != request.AudienceCountryId)
                throw new BadRequestException($"Member does not belong audience country");

            if (request.IsApplied)
                throw new BadRequestException("Cannot apply applied request");

            request.Apply();
            await _negotiationRepository.UpdateAsync(request);
            await _notifications.NegotiationRequestApplied(request.IssuerCountryId, request.AudienceCountryId, request.IssuerMemberId);
            _logger.LogInformation($"Request from {command.IssuerCountryId} to {request.AudienceCountryId} by {request.IssuerMemberId} applied");
        }
    }
}
