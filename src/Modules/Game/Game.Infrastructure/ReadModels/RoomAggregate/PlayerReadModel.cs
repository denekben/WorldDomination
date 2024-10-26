using Game.Infrastructure.ReadModels.CountryAggregate;

namespace Game.Infrastructure.ReadModels.RoomAggregate
{
    public sealed class PlayerReadModel : RoomMemberReadModel
    {
        public string GameRole { get; private set; }
        public Guid CountryId { get; private set; }
        public CountryReadModel Country { get; private set; }
    }
}
