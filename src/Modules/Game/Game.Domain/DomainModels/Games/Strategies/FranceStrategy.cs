using Game.Domain.DomainModels.Games.Strategies.DefaultStrategy;

namespace Game.Domain.DomainModels.Games.Strategies
{
    public class FranceStrategy : DefaultCountryStrategy
    {
        public override float CityDevelopmentCostCoefficient => 0.7f;
    }
}
