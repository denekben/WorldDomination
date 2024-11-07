using Game.Application.Services;
using Game.Domain.DomainModels.ReadModels.Users;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Services
{
    public class GameModuleReadService : IGameModuleReadService
    {
        private readonly GameReadDbContext _context;

        public GameModuleReadService(GameReadDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CountryExistsByNormalizedNameAsync(Guid roomId, string normalizedName)
        {
            return await _context.Countries.AnyAsync(c=>c.NormalizedName == normalizedName && c.RoomId == roomId);
        }

        public async Task<bool> GameExistsByRoomId(Guid roomId)
        {
            return await _context.Games.AnyAsync(g=>g.RoomId == roomId);
        }

        public async Task<GameUserReadModel?> GetUserAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u=>u.Id == userId);
        }
    }
}
