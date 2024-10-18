using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.ValueObjects
{
    public sealed record RoomMemberRole
    {
        public static RoomMemberRole Player => new RoomMemberRole("Player");
        public static RoomMemberRole Organizer => new RoomMemberRole("Organizer");
        public string Value { get; private set; }

        private RoomMemberRole(string value)
        {
            Value = value;
        }

        public static RoomMemberRole Create(string value)
        {
            if(value != "Player" && value != "Organizer")
            {
                throw new InvalidArgumentDomainException($"RoomMemberRole value {value} is invalid");
            }
            return new RoomMemberRole(value);
        }

        public static implicit operator string(RoomMemberRole value) => value.Value;
        public static implicit operator RoomMemberRole(string value) => Create(value);
    }
}
