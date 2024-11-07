using Game.Domain.DomainModels.Games.Strategies.DefaultStrategy;

namespace Game.Domain.DomainModels.Games.Strategies
{
    public class RussiaStrategy : DefaultCountryStrategy
    {
        public override float NuclearDefenseChance => 0.3f;
    }
}
