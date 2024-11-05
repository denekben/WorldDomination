using Game.Infrastructure.Strategies.DefaultStrategy;

namespace Game.Infrastructure.Strategies
{
    public class SwitzerlandStrategy : DefaultCountryStrategy
    {
        public override double CityIncomeCoefficient => 1.2f;
    }
}
