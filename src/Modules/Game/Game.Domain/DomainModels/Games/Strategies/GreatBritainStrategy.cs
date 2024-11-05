using Game.Infrastructure.Strategies.DefaultStrategy;

namespace Game.Infrastructure.Strategies
{
    public class GreatBritainStrategy : DefaultCountryStrategy
    {
        public override int LastHopeNuclearBombQuantity => 5;
    }
}
