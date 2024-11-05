using Game.Infrastructure.Strategies.DefaultStrategy;

namespace Game.Infrastructure.Strategies
{
    public class FranceStrategy : DefaultCountryStrategy
    {
        public override double CityDevelopmentCostCoefficient => 0.7f;
    }
}
