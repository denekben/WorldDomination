using Game.Domain.DomainModels.Messaging.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Domain.Interfaces.Repositories
{
    public interface INegotiationRequestRepository
    {
        Task<NegotiationRequest?> GetAsync(IdValueObject issuerCountryId, IdValueObject audienceCountryId);
        Task DeleteAsync(NegotiationRequest request);
        Task AddAsync(NegotiationRequest request);
        Task UpdateAsync(NegotiationRequest request);
    }
}
