namespace Game.Domain.DomainModels.Games.ValueObjects
{
    public sealed record GameState
    {
        public static GameState Debates => new GameState(nameof(Debates));
        public static GameState Negotiations => new GameState(nameof(Negotiations));
        public static GameState OrderMaking => new GameState(nameof(OrderMaking));

        public string Value { get; private set; }

        private GameState(string value)
        {
            Value = value;
        }

        public static GameState Create(string value)
        {
            return new GameState(value);
        }

        public static implicit operator string(GameState value) => value.Value;
        public static implicit operator GameState(string value) => new GameState(value);
    }
}
