using Game.Domain.DomainModels.ReadModels.Users;
using WorldDomination.Shared.Domain;

namespace Game.Application.Services
{
    public interface IGameModuleReadService
    {
        Task<bool> CountryExistsByNormalizedNameAsync(Guid roomId, string normalizedName);
        Task<GameUserReadModel?> GetUserAsync(Guid userId);
        Task<bool> GameExistsByRoomId(Guid roomId);
    }
}
