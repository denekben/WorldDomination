using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.Interfaces.Repositories;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly GameWriteDbContext _context;

        public EventRepository(GameWriteDbContext context)
        {
            _context = context;
        }

        public async Task<GameEvent> GetByQualityAsync(string quality)
        {
            var events = await _context.Events.Where(e=>e.Quality == quality).ToListAsync();

            var rand = new Random();

            return events[rand.Next(events.Count)];
        }
    }
}
