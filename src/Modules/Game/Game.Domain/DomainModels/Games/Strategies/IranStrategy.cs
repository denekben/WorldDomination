using Game.Infrastructure.Strategies.DefaultStrategy;

namespace Game.Infrastructure.Strategies
{
    public class IranStrategy : DefaultCountryStrategy
    {
        public override double NuclearBombCostCoefficient => 0.7f;
    }
}
