using Game.Domain.DomainModels.Games.Strategies.DefaultStrategy;

namespace Game.Domain.DomainModels.Games.Strategies
{
    public class ChinaStrategy : DefaultCountryStrategy
    {
        public override float SanctionPowerCoefficient => 3.0f;
    }
}
