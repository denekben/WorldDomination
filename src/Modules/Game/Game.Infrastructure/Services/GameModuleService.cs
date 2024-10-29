using Game.Application.Services;
using Game.Domain.DomainModels.RoomAggregate.Abstractions;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;
using DomainGame = Game.Domain.GameAggregate.Entities.Game;

namespace Game.Infrastructure.Services
{
    public class GameModuleService : IGameModuleService
    {
        private GameWriteDbContext _context;

        public GameModuleService(GameWriteDbContext context)
        {
            _context = context;
        }

        public async Task<DomainGame?> GetGameByRoomId(IdValueObject roomId)
        {
            return await _context.Games.FirstOrDefaultAsync(g=>g.RoomId == roomId);
        }

        public async Task<List<RoomMember>?> GetRoomMembersByRoomId(IdValueObject roomId)
        {
            return await _context.RoomMembers.Where(m=>m.RoomId == roomId).ToListAsync();
        }
    }
}
