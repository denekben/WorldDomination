using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Seed
{
    public sealed class CityPattern
    {
        public IdValueObject Id { get; private set; }
        public string CityName { get; private set; }
        public string NormalizedName { get; private set; } 
        public string CityImagePath { get; private set; }
        public bool IsCapital { get; private set; }
        public IdValueObject CountryId { get; private set; }
        public CountryPattern CountryPattern { get; private set; }

        private const string _defaultCityName = "default";
        private const string _defaultCityImagePath = "";

        //EF
        private CityPattern() { }

        public CityPattern(string? cityName, string normalizedName, string? cityImagePath, Guid countyId, bool isCapital = false)
        {
            Id = Guid.NewGuid();
            CityName = string.IsNullOrEmpty(cityName) ? _defaultCityName : cityName;
            NormalizedName = string.IsNullOrEmpty(normalizedName) ? _defaultCityName.ToUpper() : normalizedName;
            CityImagePath = string.IsNullOrEmpty(cityImagePath) ? _defaultCityImagePath : cityImagePath;
            IsCapital = isCapital;
            CountryId = countyId;
        }
    }
}
