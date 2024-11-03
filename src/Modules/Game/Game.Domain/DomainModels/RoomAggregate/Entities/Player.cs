namespace Game.Domain.DomainModels.RoomAggregate.Entities
{
    public sealed class Player : RoomMember
    {
        //EF
        private Player() { }

        private Player(Guid creatorId, Guid gameRoomId, string name, string path) 
            : base(creatorId, gameRoomId, name, path) {}

        public static Player Create(Guid creatorId, Guid gameRoomId, string name, string path)
        {
            return new Player(creatorId, gameRoomId, name, path);
        }


    }
}