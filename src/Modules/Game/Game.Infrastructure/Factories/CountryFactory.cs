using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Rooms.ValueObjects;
using Game.Domain.Interfaces.Countries;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Infrastructure.Services
{
    public class CountryFactory : ICountryFactory
    {
        private readonly GameReadDbContext _context;
        private readonly ICountryStrategyFactory _countryStrategyFactory;

        public CountryFactory(GameReadDbContext context, ICountryStrategyFactory countryStrategyFactory)
        {
            _context = context;
            _countryStrategyFactory = countryStrategyFactory;
        }

        public async Task<Country> CreateCountry(string normalizedName, Guid roomId, GameType gameType)
        {
            var countryPattern = await _context.CountryPatterns.Include(country => country.CityPatterns)
                .FirstOrDefaultAsync(country => country.NormalizedName == normalizedName)
                ?? throw new BadRequestException($"Cannot find CountryPattern with NormalizedName {normalizedName}");

            var strategy = gameType == GameType.RolePlay ? _countryStrategyFactory.CreateStrategy(countryPattern.NormalizedName) : _countryStrategyFactory.CreateStrategy();

            var country = Country.Create(
                countryPattern.CountryName, 
                countryPattern.NormalizedName, 
                countryPattern.FlagImagePath, roomId,
                strategy)
                ?? throw new BadRequestException($"Cannot create Country with NormalizedName {normalizedName}");

            foreach(var cityPattern in countryPattern.CityPatterns.OrderByDescending(country=>country.IsCapital))
            {
                country.AddCity(City.Create(cityPattern.CityName, cityPattern.NormalizedName, cityPattern.CityImagePath) 
                    ?? throw new BadRequestException($"Cannot create City {cityPattern.NormalizedName}"));
            }

            return country;
        }
    }
}
