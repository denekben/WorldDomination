using Game.Domain.DomainModels.Messaging.Entities;

namespace Game.Domain.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Task AddAsync(Message message);
    }
}
