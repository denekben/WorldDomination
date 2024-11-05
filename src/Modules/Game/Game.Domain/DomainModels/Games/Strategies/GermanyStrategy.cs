using Game.Infrastructure.Strategies.DefaultStrategy;

namespace Game.Infrastructure.Strategies
{
    public class GermanyStrategy : DefaultCountryStrategy
    {
        public override double EcologyImpactCoefficient => 0.5f;
    }
}
