using Game.Domain.DomainModels.Games.Strategies.DefaultStrategy;

namespace Game.Domain.DomainModels.Games.Strategies
{
    public class SwitzerlandStrategy : DefaultCountryStrategy
    {
        public override float CityIncomeCoefficient => 1.2f;
    }
}
