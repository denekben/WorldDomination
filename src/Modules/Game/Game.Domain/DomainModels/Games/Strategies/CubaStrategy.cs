using Game.Domain.DomainModels.Games.Strategies.DefaultStrategy;

namespace Game.Domain.DomainModels.Games.Strategies
{
    public class CubaStrategy : DefaultCountryStrategy
    {
        public override float NuclearShieldCostCoefficient => 0.8f;
    }
}
