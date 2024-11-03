﻿using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.RoomAggregate.ValueObjects
{
    public sealed record GameType
    {
        private static readonly IReadOnlyCollection<string> _gameTypes = [ "Standart", "RolePlay" ];

        public static GameType Standart => new GameType("Standart");
        public static GameType Competitive => new GameType("RolePlay");
        public string Value { get; private set; }

        private GameType(string value)
        {
            Value = value;
        }

        public static GameType Create(string value)
        {
            if (!_gameTypes.Contains(value))
            {
                throw new InvalidArgumentDomainException($"GameType value {value} is invalid");
            }

            return new GameType(value);
        }

        public static implicit operator GameType(string value) => Create(value);
        public static implicit operator string(GameType value) => value.Value;
    }
}
