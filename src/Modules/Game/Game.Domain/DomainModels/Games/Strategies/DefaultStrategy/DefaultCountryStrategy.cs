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
        private const int _defaultSanctionPower = 50;

        public int NuclearTechnologyCost => (int) (_defaultNuclearTechnologyCost * NuclearTechnologyCostCoefficient);
        public int BombCost => (int) (_defaultBombCost * NuclearBombCostCoefficient);
        public int ShieldCost => (int) (_defaultShieldCost * NuclearShieldCostCoefficient);
        public int CityDevelopmentCost => (int) (_defaultCityDevelopmentCost * CityDevelopmentCostCoefficient);
        public int EcologyDevelopmentCost => _defaultEcologyDevelopmentCost;
        public int SanctionQuantityInRoundLimit => _defualtSanctionQuantityInRoundLimit;
        public int SanctionPower => (int) (_defaultSanctionPower * SanctionPowerCoefficient);

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

        public virtual int CalculateCityIncome(Country country, City city, EcologyLevel ecologyLevel)
        {
            if (city is null)
                throw new BadRequestException("City is null");

            if (country is null)
                throw new BadRequestException("Country is null");


            if (!city.IsAlive)
            {
                if (DestroyedCityIncomeCoefficient == 0.0f)
                    return 0;
                else
                {
                    var maxIncome = country.Cities
                        .Where(c => c.IsAlive)
                        .Max(c => CityIncome(c, ecologyLevel));

                    return (int)(maxIncome * DestroyedCityIncomeCoefficient);
                }
            }
            else
            {
                return CityIncome(city, ecologyLevel);
            }
        }

        public int CalculateSanctionCost(List<Sanction> incomingSanctions)
        {
            var sanctionsPower = incomingSanctions.Sum(s => s.SanctionPower);

            return (int) (SanctionPowerImpactCoefficient * sanctionsPower);
        }

        private int CityIncome(City city, EcologyLevel ecologyLevel)
        {
            return city.IsAlive 
                ? (int)(city.DevelopmentLevel
                    * (100 - EcologyImpactCoefficient * (100 - ecologyLevel))
                    * CityIncomeCoefficient
                    * _globalIncomeCoefficient)
                : 0;
        }
    }
}
