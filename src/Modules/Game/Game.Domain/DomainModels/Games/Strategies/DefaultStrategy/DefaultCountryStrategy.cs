using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Games.ValueObjects;
using Game.Domain.Interfaces.Countries;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.Strategies.DefaultStrategy
{
    public class DefaultCountryStrategy : ICountryStrategy
    {
        private const float _globalIncomeCoefficient = 0.03f;
        private const int _defaultNuclearTechnologyCost = 500;
        private const int _defaultBombCost = 150;
        private const int _defaultShieldCost = 300;
        private const int _defaultCityDevelopmentCost = 150;
        private const int _defaultEcologyDevelopmentCost = 50;
        private const int _defualtSanctionQuantityInRoundLimit = 2;
        private const float _defaultSanctionPower = 1.0f;

        public int NuclearTechnologyCost => (int) (_defaultNuclearTechnologyCost * NuclearTechnologyCostCoefficient);
        public int BombCost => (int) (_defaultBombCost * NuclearBombCostCoefficient);
        public int ShieldCost => (int) (_defaultShieldCost * NuclearShieldCostCoefficient);
        public int CityDevelopmentCost => (int) (_defaultCityDevelopmentCost * CityDevelopmentCostCoefficient);
        public int EcologyDevelopmentCost => _defaultEcologyDevelopmentCost;
        public int SanctionQuantityInRoundLimit => _defualtSanctionQuantityInRoundLimit;
        public float SanctionPower => _defaultSanctionPower * SanctionPowerCoefficient;

        public virtual float SanctionPowerCoefficient => 1.0f;
        public virtual float EcologyImpactCoefficient => 1.0f;
        public virtual float CityDevelopmentCostCoefficient => 1.0f;
        public virtual float NuclearTechnologyCostCoefficient => 1.0f;
        public virtual float NuclearBombCostCoefficient => 1.0f;
        public virtual float NuclearShieldCostCoefficient => 1.0f;
        public virtual float CityIncomeCoefficient => 1.0f;
        public virtual float NuclearDefenseChance => 0.0f;
        public virtual float SanctionPowerImpactCoefficient => 0.1f;
        public virtual float DestroyedCityIncomeCoefficient => 0.0f;
        public virtual int LastHopeNuclearBombQuantity => 0;

        public virtual int CalculateCityIncome(Country country, City city, EcologyLevel ecologyLevel, List<Sanction> incomingSanctions)
        {
            if (city is null)
                throw new BadRequestException("City is null");

            if (country is null)
                throw new BadRequestException("Country is null");

            var sanctionsPower = incomingSanctions.Sum(s => s.SanctionPower);

            if (!city.IsAlive)
            {
                if (DestroyedCityIncomeCoefficient == 0.0f)
                    return 0;
                else
                {
                    var maxIncome = country.Cities
                        .Where(c => c.IsAlive)
                        .Max(c => CityIncome(c, ecologyLevel, sanctionsPower));

                    return (int)(maxIncome * DestroyedCityIncomeCoefficient);
                }
            }
            else
            {
                return CityIncome(city, ecologyLevel, sanctionsPower);
            }
        }

        private int CityIncome(City city, EcologyLevel ecologyLevel, float sanctionsPower)
        {
            return city.IsAlive 
                ? (int) (city.DevelopmentLevel
                    * (1 - EcologyImpactCoefficient * (1 - ecologyLevel))
                    * (1 - SanctionPowerImpactCoefficient * sanctionsPower)
                    * CityIncomeCoefficient
                    * _globalIncomeCoefficient)
                : 0;
        }
    }
}
