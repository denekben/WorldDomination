using Game.Domain.DomainModels.Games.Strategies.DefaultStrategy;

namespace Game.Domain.DomainModels.Games.Strategies
{
    public class IranStrategy : DefaultCountryStrategy
    {
        public override float NuclearBombCostCoefficient => 0.7f;
    }
}
