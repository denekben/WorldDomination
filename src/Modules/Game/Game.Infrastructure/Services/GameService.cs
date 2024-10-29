using Game.Application.Services;
using Game.Domain.ReadModels.GameAggregate;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private DbSet<GameReadModel> _games;

        public GameService(GameReadDbContext context)
        {
            _games = context.Games;
        }

        public async Task<GameReadModel?> GetGameByRoomId(Guid roomId)
        {
            return await _games.FirstOrDefaultAsync(g=>g.RoomId == roomId);
        }
    }
}
