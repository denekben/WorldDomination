namespace Game.Domain.Interfaces.Countries
{
    public interface ICountryStrategyFactory
    {
        ICountryStrategy CreateStrategy(string? normalizedName = null);
    }
}
