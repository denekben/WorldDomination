using Game.Infrastructure.Strategies.DefaultStrategy;

namespace Game.Infrastructure.Strategies
{
    public class RussiaStrategy : DefaultCountryStrategy
    {
        public override double NuclearDefenseChance => 0.3f;
    }
}
