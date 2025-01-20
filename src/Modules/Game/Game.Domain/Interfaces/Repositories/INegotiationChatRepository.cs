using Game.Domain.DomainModels.Messaging.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Domain.Interfaces.Repositories
{
    public interface INegotiationChatRepository
    {
        Task<NegotiationChat?> GetAsync(IdValueObject firstCountryId, IdValueObject secondCountryId);
        Task<NegotiationChat?> GetAsync(IdValueObject chatId);
        Task UpdateAsync(NegotiationChat chat);
    }
}
