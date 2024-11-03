﻿using Game.Domain.DomainModels.ReadModels.GameAggregate;
using Game.Domain.DomainModels.ReadModels.UserAggregate;
using Game.Domain.DomainModels.RoomAggregate.ValueObjects;

namespace Game.Domain.DomainModels.ReadModels.RoomAggregate
{
    public sealed class RoomReadModel
    {
        public Guid Id { get; private set; }
        public string RoomName { get; private set; }
        public string GameType { get; private set; }
        public bool HasTeams { get; private set; }
        public int RoomMemberLimit { get; private set; }
        public int CountryLimit { get; private set; }
        public bool IsPrivate { get; private set; }
        public string RoomCode { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public GameReadModel Game { get; private set; }
        public Guid CreatorId { get; private set; }
        public GameUserReadModel Creator { get; private set; }
        public List<CountryReadModel> Countries { get; private set; }
        public List<RoomMemberReadModel> RoomMembers { get; private set; }
    }
}
