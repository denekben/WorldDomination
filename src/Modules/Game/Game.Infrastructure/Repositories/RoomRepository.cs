using Game.Domain.RoomAggregate.Entities;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private readonly DbSet<Room> _rooms;
        private readonly GameWriteDbContext _context;

        public RoomRepository(GameWriteDbContext context)
        {
            _context = context;
            _rooms = context.Rooms;
        }

        public async Task AddAsync(Room room)
        {
            await _rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Room room)
        {
            _rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room?> GetAsync(IdValueObject id)
        {
            var room = await _rooms.Include(r=>r.RoomMembers).Include(r=>r.Creator).Include(r=>r.DomainGame).FirstOrDefaultAsync(r => r.Id == id);
            return room;
        }

        public async Task UpdateAsync(Room room)
        {
            _rooms.Update(room);
            await _context.SaveChangesAsync();
        }
    }
}
