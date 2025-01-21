using Game.Domain.DomainModels.Games.Entities;

namespace Game.Domain.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<GameEvent> GetByQualityAsync(string quality);
    }
}
