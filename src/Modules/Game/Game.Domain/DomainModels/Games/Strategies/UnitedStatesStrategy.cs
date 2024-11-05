using Game.Infrastructure.Strategies.DefaultStrategy;

namespace Game.Infrastructure.Strategies
{
    public class UnitedStatesStrategy : DefaultCountryStrategy
    {
        public override double NuclearTechnologyCostCoefficient => 0.4f;
    }
}
