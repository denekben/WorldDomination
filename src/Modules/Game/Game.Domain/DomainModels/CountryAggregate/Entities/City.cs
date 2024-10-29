using Game.Domain.CountryAggregate.ValueObjects;
using WorldDomination.Shared.Domain;

namespace Game.Domain.CountryAggregate.Entities
{
    public sealed class City : DomainEntity
    {
        public IdValueObject Id {  get; private set; }
        public string CityName { get; private set; }
        public string CityImagePath { get; private set; }
        public bool IsAlive { get; private set; }
        public bool HaveShield { get; private set; }
        public DevelopmentLevel DevelopmentLevel { get; private set; }
        public LivingLevel LivingLevel { get; private set; }
        public Income Income { get; private set; }

        public IdValueObject CountryId { get; private set; }
        public Country Country { get; private set; }

        //EF
        private City() { }

        private City(string cityName, string cityImagePath, Guid countryId)
        {
            Id = Guid.NewGuid();
            CityName = cityName;
            CityImagePath = cityImagePath;
            IsAlive = true;
            HaveShield = false;
            DevelopmentLevel = DevelopmentLevel.Create();
            LivingLevel = LivingLevel.Create();
            Income = Income.Create();
            CountryId = countryId;
        }

        public static City Create(string cityName, string cityImagePath, Guid countryId)
        {
            return new City(cityName, cityImagePath, countryId);
        }
    }
}
