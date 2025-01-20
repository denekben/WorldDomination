using Game.Domain.DomainModels.ReadModels.Rooms;
using Game.Domain.DomainModels.ReadModels.Users;
using Game.Domain.ReadModels.Messaging;

namespace Game.Application.Services
{
    public interface IGameModuleReadService
    {
        // Country
        Task<bool> CountryExistsByNormalizedNameAsync(Guid roomId, string normalizedName);
        Task<bool> CountryExistsByIdAsync(Guid countryId);

        // User
        Task<GameUserReadModel?> GetUserAsync(Guid userId);

        // RoomMeber
        Task<RoomMemberReadModel?> GetRoomMemberAsync(Guid memberId, Guid roomId);
        Task<bool> RoomMemberExistsByUserIdAsync(Guid userId);

        // Game
        Task<bool> GameExistsByRoomIdAsync(Guid roomId);

        // Negotioations
        Task<bool> NegotationChannelExists(Guid firstCountryId, Guid secondCountryId);
        Task<NegotiationRequestReadModel?> GetNegotiationRequestByChannelAsync(Guid firstCountryId, Guid secondCountryId);
    }
}
