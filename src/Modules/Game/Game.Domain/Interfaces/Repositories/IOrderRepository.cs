using Game.Domain.DomainModels.Games.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetAsync(IdValueObject countryId);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
