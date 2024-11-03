namespace Game.Infrastructure.Seed
{
    public sealed class CountryPatternReadModel
    {
        public Guid Id { get; private set; }
        public string CountryName { get; private set; }
        public string NormalizedName { get; private set; }
        public string FlagImagePath { get; private set; }
        public List<CityPatternReadModel> CityPatterns { get; private set; }
    }
}
