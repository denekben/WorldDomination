using Game.Domain.Interfaces.Countries;
using Game.Infrastructure.Strategies.DefaultStrategy;
using Game.Infrastructure.Strategies;
using Microsoft.Extensions.DependencyInjection;

public class CountryStrategyFactory : ICountryStrategyFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, Type> _strategyMap;

    public CountryStrategyFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _strategyMap = new Dictionary<string, Type>
        {
            ["CHINA"] = typeof(ChinaStrategy),
            ["CUBA"] = typeof(CubaStrategy),
            ["FRANCE"] = typeof(FranceStrategy),
            ["GERMANY"] = typeof(GermanyStrategy),
            ["GREAT_BRITAIN"] = typeof(GreatBritainStrategy),
            ["IRAN"] = typeof(IranStrategy),
            ["JAPAN"] = typeof(JapanStrategy),
            ["NORTH_KOREA"] = typeof(NorthKoreaStrategy),
            ["RUSSIA"] = typeof(RussiaStrategy),
            ["SWITZERLAND"] = typeof(SwitzerlandStrategy),
            ["UNITED_STATES"] = typeof(UnitedStatesStrategy)
        };
    }

    public ICountryStrategy CreateStrategy(string? normalizedName = null)
    {
        if (string.IsNullOrEmpty(normalizedName))
        {
            return GetStrategy<DefaultCountryStrategy>();
        }

        return _strategyMap.TryGetValue(normalizedName, out var strategyType)
            ? GetStrategy(strategyType)
            : GetStrategy<DefaultCountryStrategy>();
    }

    private ICountryStrategy GetStrategy<T>() where T : ICountryStrategy
    {
        return _serviceProvider.GetRequiredService<T>();
    }

    private ICountryStrategy GetStrategy(Type strategyType)
    {
        return (ICountryStrategy)_serviceProvider.GetRequiredService(strategyType);
    }
}