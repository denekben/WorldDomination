using Game.Application.Services;
using Game.Domain.DomainModels.GameAggregate.Entities;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Infrastructure.Services
{
    public class CountryFabric : ICountryFabric
    {
        private readonly GameReadDbContext _context;

        public CountryFabric(GameReadDbContext context)
        {
            _context = context;
        }

        public async Task<Country> CreateCountry(string normalizedName, Guid roomId)
        {
            var countryPattern = await _context.CountryPatterns.Include(country => country.CityPatterns)
                .FirstOrDefaultAsync(country => country.NormalizedName == normalizedName)
                ?? throw new BadRequestException($"Cannot find CountryPattern with NormalizedName {normalizedName}");

            var country = Country.Create(countryPattern.CountryName, countryPattern.NormalizedName, countryPattern.FlagImagePath, roomId)
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
