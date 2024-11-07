using Game.Domain.DomainModels.Games.ValueObjects;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.Entities
{
    public sealed class City : DomainEntity
    {
        private const int _developmentLevelIncreasing = 20;

        public IdValueObject Id {  get; private set; }
        public string CityName { get; private set; }
        public string NormalizedName { get; private set; }
        public string CityImagePath { get; private set; }
        public bool IsAlive { get; private set; }
        public bool HaveShield { get; private set; }
        public DevelopmentLevel DevelopmentLevel { get; private set; }
        public LivingLevel LivingLevel { get; private set; }

        public IdValueObject CountryId { get; private set; }
        public Country Country { get; private set; }

        //EF
        private City() { }

        private City(string cityName, string normalizedName, string cityImagePath)
        {
            Id = Guid.NewGuid();
            CityName = cityName;
            NormalizedName = normalizedName;
            CityImagePath = cityImagePath;
            IsAlive = true;
            HaveShield = false;
            DevelopmentLevel = DevelopmentLevel.Create();
            LivingLevel = LivingLevel.Create();
        }

        public static City Create(string cityName, string normalizedName, string cityImagePath)
        {
            return new City(cityName, normalizedName, cityImagePath);
        }

        public void DevelopCity()
        {
            DevelopmentLevel += _developmentLevelIncreasing;
        }

        internal void SetShield()
        {
            if (HaveShield)
                throw new BusinessRuleValidationException("Cannot set a shield to a City with shield");

            if (!IsAlive)
                throw new BusinessRuleValidationException("Can set shield only for alive Cities");
        }

        internal void GetStrike()
        {
            if (HaveShield)
                HaveShield = false;
            else
                IsAlive = false;
        }
    }
}
