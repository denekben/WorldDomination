using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.Interfaces.Repositories;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbSet<Order> _orders;
        private readonly GameWriteDbContext _context;

        public OrderRepository(GameWriteDbContext context)
        {
            _orders = context.Orders;
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetAsync(IdValueObject countryId)
        {
            return await _orders.FirstOrDefaultAsync(o => o.CountryId == countryId);
        }

        public async Task UpdateAsync(Order order)
        {
            _orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
