using Game.Domain.DomainModels.Games.Strategies.DefaultStrategy;

namespace Game.Domain.DomainModels.Games.Strategies
{
    public class JapanStrategy : DefaultCountryStrategy
    {
        public override float DestroyedCityIncomeCoefficient => 0.5f;
    }
}
