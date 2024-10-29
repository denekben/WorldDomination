using Game.Domain.ReadModels.GameAggregate;

namespace Game.Application.Services
{
    public interface IGameService
    {
        Task<GameReadModel?> GetGameByRoomId(Guid roomId);
    }
}
