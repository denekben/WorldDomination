using WorldDomination.Shared.Domain;

namespace Game.Domain.Entities
{
    public sealed class Game
    {
        public IdValueObject Id { get; private set; }
        public IdValueObject RoomId { get; private set; }
        public Room Room { get; private set; }
    }
}
