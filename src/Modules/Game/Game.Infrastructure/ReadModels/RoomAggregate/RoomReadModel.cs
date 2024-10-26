﻿using Game.Infrastructure.ReadModels.GameAggregate;
using Game.Infrastructure.ReadModels.UserAggregate;

namespace Game.Infrastructure.ReadModels.RoomAggregate
{
    public sealed class RoomReadModel
    {
        public Guid Id { get; private set; }
        public string RoomName { get; private set; }
        public string GameType { get; private set; }
        public int RoomMemberLimit { get; private set; }
        public int CountryQuantity { get; private set; }
        public bool IsPublic { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public GameReadModel Game { get; private set; }
        public Guid CreatorId { get; private set; }
        public GameUserReadModel Creator { get; private set; }
        public List<RoomMemberReadModel> RoomMembers { get; private set; }
    }
}