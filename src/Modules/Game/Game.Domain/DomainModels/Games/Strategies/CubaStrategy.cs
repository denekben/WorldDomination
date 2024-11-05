using Game.Infrastructure.Strategies.DefaultStrategy;

namespace Game.Infrastructure.Strategies
{
    public class CubaStrategy : DefaultCountryStrategy
    {
        public override double NuclearShieldCostCoefficient => 0.8f;
    }
}
