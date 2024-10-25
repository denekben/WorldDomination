using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace User.Domain.ValueObjects
{
    public sealed record ActivityStatus
    {
        private static readonly IReadOnlyCollection<string> _allowedStatuses = ["Online", "Offlie", "InGame", "InLobby"];

        public string Value { get; private set; }
        public static ActivityStatus Online => new ActivityStatus("Online");
        public static ActivityStatus Offline => new ActivityStatus("Offline");
        public static ActivityStatus InGame => new ActivityStatus("InGame");
        public static ActivityStatus InLobby => new ActivityStatus("InLobby");

        private ActivityStatus(string value)
        {
            Value = value;
        }

        public static ActivityStatus Create(string value)
        {
            if (!_allowedStatuses.Contains(value))
            {
                throw new InvalidArgumentDomainException($"Cannot create ActivityStatus with {value}");
            }
            return new ActivityStatus(value);
        }

        public static implicit operator ActivityStatus(string value) => Create(value);

        public static implicit operator string(ActivityStatus value) => value.Value;
    }
}
