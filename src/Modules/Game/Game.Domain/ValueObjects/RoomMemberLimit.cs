using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.ValueObjects
{
    public sealed record RoomMemberLimit
    {
        public int Value { get; private set; }

        private RoomMemberLimit(int value) {
            Value = value;
        }

        public static RoomMemberLimit Create(int value)
        {
            if(value < 1 || value > 40)
            {
                throw new InvalidArgumentDomainException($"MemberLimit {value} is invalid");
            }

            return new RoomMemberLimit(value);
        }

        public static implicit operator int(RoomMemberLimit value) => value.Value;
        public static implicit operator RoomMemberLimit(int value) => Create(value);
    }
}
