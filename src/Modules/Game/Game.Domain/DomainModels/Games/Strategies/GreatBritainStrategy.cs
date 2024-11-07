using Game.Domain.DomainModels.Games.Strategies.DefaultStrategy;

namespace Game.Domain.DomainModels.Games.Strategies
{
    public class GreatBritainStrategy : DefaultCountryStrategy
    {
        public override int LastHopeNuclearBombQuantity => 5;
    }
}
