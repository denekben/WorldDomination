namespace Game.Domain.DomainModels.Games.ValueObjects
{
    public sealed class GameEventQuality
    {
        public string Value { get; private set; }
        public static GameEventQuality Neutral => new GameEventQuality(nameof(Neutral));
        public static GameEventQuality Bad => new GameEventQuality(nameof(Bad));
        public static GameEventQuality Good => new GameEventQuality(nameof(Good));

        private GameEventQuality(string value)
        {
            Value = value;
        }

        public static GameEventQuality Create(string value)
        {
            return new GameEventQuality(value);
        }

        public static implicit operator string(GameEventQuality value) => value.Value;
        public static implicit operator GameEventQuality(string value) => new GameEventQuality(value);
    }
}
