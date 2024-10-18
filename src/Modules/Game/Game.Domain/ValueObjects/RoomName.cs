namespace Game.Domain.ValueObjects
{
    public sealed record RoomName
    {
        public string Value { get; private set; }

        private RoomName(string value)
        {
            Value = value;
        }

        public static RoomName Create(string value)
        {
            return new RoomName(value);
        }

        public static implicit operator string(RoomName value) => value.Value;
        public static implicit operator RoomName(string value) => Create(value);
    }
}
