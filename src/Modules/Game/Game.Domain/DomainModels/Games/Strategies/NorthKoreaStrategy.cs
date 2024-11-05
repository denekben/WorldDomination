using Game.Infrastructure.Strategies.DefaultStrategy;

namespace Game.Infrastructure.Strategies
{
    public class NorthKoreaStrategy : DefaultCountryStrategy
    {
        public override double SanctionPowerImpactCoefficient => 0.0f;
    }
}
