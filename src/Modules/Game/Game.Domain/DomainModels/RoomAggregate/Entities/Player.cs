using Game.Domain.CountryAggregate.Entities;
using Game.Domain.DomainModels.RoomAggregate.Abstractions;
using Game.Domain.RoomAggregate.Entities;
using Game.Domain.RoomAggregate.ValueObjects;
using Game.Domain.UserAggregate.Entities;
using System.IO;
using WorldDomination.Shared.Domain;

namespace Game.Domain.RoomAggregate.Entities
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