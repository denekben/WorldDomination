using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Games.ValueObjects;

namespace Game.Domain.Interfaces.Countries
{
    public interface ICountryStrategy
    {
        int NuclearTechnologyCost { get; }
        int BombCost {  get; }
        int ShieldCost { get; }
        int CityDevelopmentCost { get; }
        int EcologyDevelopmentCost { get; }
        int SanctionQuantityInRoundLimit { get; }
        int SanctionPower { get; }

        float NuclearDefenseChance { get; } 
        float SanctionPowerCoefficient { get; } 
        float DestroyedCityIncomeCoefficient { get; } 
        float EcologyImpactCoefficient { get; }
        float CityDevelopmentCostCoefficient { get; } 
        float NuclearTechnologyCostCoefficient { get; } 
        float SanctionPowerImpactCoefficient { get; } 
        float NuclearBombCostCoefficient { get; }
        float NuclearShieldCostCoefficient { get; }
        float CityIncomeCoefficient { get; }

        int CalculateCityIncome(Country country, City city, EcologyLevel ecologyLevel);

        int CalculateSanctionCost(List<Sanction> incomingSanctions);
    }
}
