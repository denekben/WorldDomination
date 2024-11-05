using Game.Infrastructure.Strategies.DefaultStrategy;

namespace Game.Infrastructure.Strategies
{
    public class ChinaStrategy : DefaultCountryStrategy
    {
        public override double SanctionPowerCoefficient => 3.0f;
    }
}
