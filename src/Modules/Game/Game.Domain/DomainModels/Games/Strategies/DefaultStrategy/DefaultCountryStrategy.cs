using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Games.ValueObjects;
using Game.Domain.Interfaces.Countries;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Infrastructure.Strategies.DefaultStrategy
{
    public class DefaultCountryStrategy : ICountryStrategy
    {
        private const float _globalIncomeCoefficient = 0.03f;
        private const int _defaultNuclearTechnologyCost = 500;
        private const int _defaultBombCost = 150;
        private const int _defaultShieldCost = 300;
        private const int _defaultDevelopmentCost = 150;

        public int NuclearTechnologyCost => (int) (_defaultNuclearTechnologyCost * NuclearTechnologyCostCoefficient);
        public int BombCost => (int) (_defaultBombCost * NuclearBombCostCoefficient);
        public int ShieldCost => (int) (_defaultShieldCost * NuclearShieldCostCoefficient);
        public int CityDevelopmentCost => (int) (_defaultDevelopmentCost * CityDevelopmentCostCoefficient);

        public virtual double SanctionPowerCoefficient => 1.0f;
        public virtual double EcologyImpactCoefficient => 1.0f;
        public virtual double CityDevelopmentCostCoefficient => 1.0f;
        public virtual double NuclearTechnologyCostCoefficient => 1.0f;
        public virtual double NuclearBombCostCoefficient => 1.0f;
        public virtual double NuclearShieldCostCoefficient => 1.0f;
        public virtual double CityIncomeCoefficient => 1.0f;
        public virtual double NuclearDefenseChance => 0.0f;
        public virtual double SanctionPowerImpactCoefficient => 0.1f;
        public virtual double DestroyedCityIncomeCoefficient => 0.0f;
        public virtual int LastHopeNuclearBombQuantity => 0;

        public virtual int CalculateCityIncome(Country country, City city, EcologyLevel ecologyLevel, List<Sanction> sanctions)
        {
            if (city is null)
                throw new BadRequestException("City is null");

            if (country is null)
            {
                throw new BadRequestException("Country is null");
            }

            if (!city.IsAlive)
            {
                if (DestroyedCityIncomeCoefficient == 0.0f)
                    return 0;
                else
                {
                    var maxIncome = country.Cities
                        .Where(c => c.IsAlive)
                        .Max(c => c.Income.Value);

                    return (int)(maxIncome * DestroyedCityIncomeCoefficient);
                }
            }
            else
            {
                var sanctionsPower = sanctions.Sum(s=>s.SanctionPower);

                return (int) (city.DevelopmentLevel 
                    * (1 - EcologyImpactCoefficient * (1 - ecologyLevel)) 
                    * (1 - SanctionPowerImpactCoefficient * sanctionsPower)
                    * CityIncomeCoefficient
                    * _globalIncomeCoefficient);
            }
        }
    }
}
