using Game.Infrastructure.Strategies.DefaultStrategy;

namespace Game.Infrastructure.Strategies
{
    public class JapanStrategy : DefaultCountryStrategy
    {
        public override double DestroyedCityIncomeCoefficient => 0.5f;
    }
}
