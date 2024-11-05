using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.Interfaces.Repositories;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
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

        public async Task<Room?> GetAsync(IdValueObject id, RoomIncludes includes)
        {
            IQueryable<Room> query = _rooms;

            if (includes.HasFlag(RoomIncludes.RoomMembers))
                query = query.Include(r => r.RoomMembers);
            if(includes.HasFlag(RoomIncludes.Countries))
                query = query.Include(r => r.Countries);
            if(includes.HasFlag(RoomIncludes.Creator))
                query = query.Include(r => r.Creator);
            if(includes.HasFlag(RoomIncludes.DomainGame))
                query = query.Include(r => r.DomainGame);

            var room = await query.FirstOrDefaultAsync(r => r.Id == id);
            return room;
        }

        public async Task UpdateAsync(Room room)
        {
            _rooms.Update(room);
            await _context.SaveChangesAsync();
        }
    }
}

