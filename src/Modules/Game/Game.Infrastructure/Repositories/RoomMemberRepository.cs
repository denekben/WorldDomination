using Game.Domain.DomainModels.RoomAggregate.Abstractions;
using Game.Domain.RoomAggregate.Entities;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    public class RoomMemberRepository : IRepository<RoomMember>
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

        public async Task<RoomMember?> GetAsync(IdValueObject id)
        {
            return await _members.FirstOrDefaultAsync(m=>m.GameUserId == id);
        }

        public async Task UpdateAsync(RoomMember member)
        {
            _members.Update(member);
            await _context.SaveChangesAsync();
        }
    }
}