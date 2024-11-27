using Game.Domain.Interfaces.Repositories;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;
using DomainGame = Game.Domain.DomainModels.Games.Entities.Game;

namespace Game.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly DbSet<DomainGame> _games;
        private readonly IServiceProvider _provider;

        public GameRepository(GameWriteDbContext context)
        {
            _games = context.Games ;
        }

        public Task AddAsync(DomainGame user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DomainGame user)
        {
            throw new NotImplementedException();
        }

        public async Task<DomainGame?> GetAsync(IdValueObject roomId)
        {
            return await _games.FirstOrDefaultAsync(g=>g.RoomId == roomId);
        }

        public async Task<DomainGame?> GetAsync(IdValueObject roomId, GameIncludes includes)
        {
            IQueryable<DomainGame> query = _games;

            if (includes.HasFlag(GameIncludes.Countries))
                query.Include(q => q.Countries);

            if (includes.HasFlag(GameIncludes.CountriesWithCities))
                query.Include(q => q.Countries).ThenInclude(c => c.Cities);

            var game =  await query.FirstOrDefaultAsync(q=>q.RoomId == roomId);

            if(game!=null && (includes.HasFlag(GameIncludes.Countries) 
                || includes.HasFlag(GameIncludes.CountriesWithCities)))
            {
                foreach(var country in game.Countries)
                {
                    country.InitializeStrategy(_provider);
                }
            }

            return game;

        }

        public Task UpdateAsync(DomainGame user)
        {
            throw new NotImplementedException();
        }
    }
}
