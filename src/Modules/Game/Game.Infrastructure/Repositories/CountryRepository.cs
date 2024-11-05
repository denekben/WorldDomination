using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.Interfaces.Repositories;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DbSet<Country> _countries;
        private readonly GameWriteDbContext _context;

        public CountryRepository(GameWriteDbContext context)
        {
            _countries = context.Countries;
            _context = context;
        }

        public async Task AddAsync(Country country)
        {
            await _countries.AddAsync(country);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Country country)
        {
            _countries.Remove(country);
            await _context.SaveChangesAsync();
        }

        public async Task<Country?> GetAsync(IdValueObject id)
        {
            return await _countries.FirstOrDefaultAsync(c=>c.Id == id);
        }

        public async Task<Country?> GetAsync(IdValueObject id, CountryIncludes includes)
        {
            IQueryable<Country> query = _countries;

            if (includes.HasFlag(CountryIncludes.Players))
                query = query.Include(c=>c.Players);

            return await query.FirstOrDefaultAsync(c=>c.Id == id);
        }

        public async Task UpdateAsync(Country country)
        {
            _countries.Update(country);
            await _context.SaveChangesAsync();
        }
    }
}
