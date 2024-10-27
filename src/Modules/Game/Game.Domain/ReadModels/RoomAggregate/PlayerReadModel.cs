using Game.Domain.ReadModels.CountryAggregate;

namespace Game.Domain.ReadModels.RoomAggregate
{
    public sealed class PlayerReadModel : RoomMemberReadModel
    {
        public string GameRole { get; private set; }
        public Guid CountryId { get; private set; }
        public CountryReadModel Country { get; private set; }
    }
}
