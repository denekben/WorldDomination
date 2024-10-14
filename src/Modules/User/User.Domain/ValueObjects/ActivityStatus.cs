using User.Domain.Exceptions;

namespace User.Domain.ValueObjects
{
    public sealed record ActivityStatus
    {
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
            value = value.ToLower();
            if (value != "online" && value != "offline" && value != "ingame" && value != "inlobby")
            {
                throw new InvalidArgumentDomainException($"Cannot create ActivityStatus with {value}");
            }
            return new ActivityStatus(value);
        }

        public static implicit operator ActivityStatus(string value)
            => Create(value);

        public static implicit operator string(ActivityStatus value)
            => value.Value;
    }
}
