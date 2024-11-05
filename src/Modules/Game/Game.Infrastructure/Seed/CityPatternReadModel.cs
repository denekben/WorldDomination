namespace Game.Infrastructure.Seed
{
    public sealed class CityPatternReadModel
    {
        public Guid Id { get; private set; }
        public string CityName { get; private set; }
        public string NormalizedName { get; private set; }
        public string CityImagePath { get; private set; }
        public bool IsCapital { get; private set; }
        public Guid CountryId { get; private set; }
        public CountryPatternReadModel CountryPattern { get; private set; }
    }
}
