using Game.Domain.DomainModels.Games.Strategies.DefaultStrategy;

namespace Game.Domain.DomainModels.Games.Strategies
{
    public class UnitedStatesStrategy : DefaultCountryStrategy
    {
        public override float NuclearTechnologyCostCoefficient => 0.4f;
    }
}
