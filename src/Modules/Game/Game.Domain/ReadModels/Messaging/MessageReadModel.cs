using Game.Domain.DomainModels.ReadModels.Rooms;

namespace Game.Domain.ReadModels.Messaging
{
    public sealed class MessageReadModel
    {
        public Guid Id { get; private set; }
        public string MessageText { get; private set; }
        public Guid IssuerId { get; private set; }
        public RoomMemberReadModel Issuer { get; private set; }
        public Guid ChatId { get; private set; }
    }
}
