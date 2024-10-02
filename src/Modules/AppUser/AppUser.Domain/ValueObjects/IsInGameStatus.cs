namespace AppUser.Domain.ValueObjects
{
    public sealed record IsInGameStatus
    {
        public string Value {  get; private set; }
        public static IsInGameStatus InGame => new IsInGameStatus(nameof(InGame));
        public static IsInGameStatus NotInGame => new IsInGameStatus(nameof(NotInGame));

        public IsInGameStatus(string value) {
            Value = value;
        }

    }
}
