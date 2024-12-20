﻿using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Rooms.ValueObjects
{
    public sealed record RoomMemberLimit
    {
        private const int _maxLimit = 50;
        private const int _minLimit = 2;
        private const int _defaultLimit = 50;

        public int Value { get; private set; }

        private RoomMemberLimit(int value)
        {
            Value = value;
        }

        public static RoomMemberLimit Create(int value = _defaultLimit)
        {
            if (value < _minLimit || value > _maxLimit)
                throw new InvalidArgumentDomainException($"MemberLimit {value} is invalid");

            return new RoomMemberLimit(value);
        }

        public static implicit operator int(RoomMemberLimit value) => value.Value;
        public static implicit operator RoomMemberLimit(int value) => Create(value);
    }
}
