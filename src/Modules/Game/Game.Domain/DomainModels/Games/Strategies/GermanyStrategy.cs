using Game.Domain.DomainModels.Games.Strategies.DefaultStrategy;

namespace Game.Domain.DomainModels.Games.Strategies
{
    public class GermanyStrategy : DefaultCountryStrategy
    {
        public override float EcologyImpactCoefficient => 0.5f;
    }
}
