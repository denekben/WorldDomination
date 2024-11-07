using Game.Domain.DomainModels.Games.Strategies.DefaultStrategy;

namespace Game.Domain.DomainModels.Games.Strategies
{
    public class NorthKoreaStrategy : DefaultCountryStrategy
    {
        public override float SanctionPowerImpactCoefficient => 0.0f;
    }
}
