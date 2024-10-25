using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Seed
{
    public sealed class CountryPattern
    {
        public IdValueObject Id { get; private set; }
        public string CountryName { get; private set; }
        public string FlagImagePath { get; private set; }
        public List<CityPattern> CityPatterns { get; private set; }
        private const string _defaultCountryName = "default";
        private const string _defaultFlagImagePath = "";

        //EF
        private CountryPattern() { }

        public CountryPattern(string? countryName, string? flagImagePath)
        {
            Id = Guid.NewGuid();
            CountryName = string.IsNullOrEmpty(countryName) ? _defaultCountryName : countryName;
            FlagImagePath = string.IsNullOrEmpty(flagImagePath) ? _defaultFlagImagePath : flagImagePath;
        }
    }

}