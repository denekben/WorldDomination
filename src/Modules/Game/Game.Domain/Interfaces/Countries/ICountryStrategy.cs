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

        double NuclearDefenseChance { get; } 
        double SanctionPowerCoefficient { get; } 
        double DestroyedCityIncomeCoefficient { get; } 
        double EcologyImpactCoefficient { get; }
        double CityDevelopmentCostCoefficient { get; } 
        double NuclearTechnologyCostCoefficient { get; } 
        double SanctionPowerImpactCoefficient { get; } 
        double NuclearBombCostCoefficient { get; }
        double NuclearShieldCostCoefficient { get; }
        double CityIncomeCoefficient { get; }

        int CalculateCityIncome(Country country, City city, EcologyLevel ecologyLevel, List<Sanction> sanctions);
    }
}
