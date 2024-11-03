using Game.Domain.DomainModels.RoomAggregate.Entities;
using Game.Domain.Repositories;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    public class RoomMemberRepository : IRoomMemberRepository
    {
        private readonly GameWriteDbContext _context;
        private readonly DbSet<RoomMember> _members;

        public RoomMemberRepository(GameWriteDbContext context)
        {
            _context = context;
            _members = context.Members;
        }

        public async Task AddAsync(RoomMember member)
        {
            await _members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RoomMember member)
        {
            _members.Remove(member);
            await _context.SaveChangesAsync();
        }

        public async Task<RoomMember?> GetAsync(IdValueObject gameUserId, IdValueObject roomId)
        {
            return await _members.FirstOrDefaultAsync(m => (m.GameUserId == gameUserId && m.RoomId == roomId));
        }

        public async Task<RoomMember?> GetAsync(IdValueObject gameUserId, IdValueObject roomId, RoomMemberIncludes includes)
        {
            IQueryable<RoomMember> query = _members;

            if (includes.HasFlag(RoomMemberIncludes.Country))
                query = query.Include(m => m.Country);

            return await query.FirstOrDefaultAsync(m => (m.GameUserId == gameUserId && m.RoomId == roomId));
        }

        public async Task UpdateAsync(RoomMember member)
        {
            _members.Update(member);
            await _context.SaveChangesAsync();
        }
    }
}