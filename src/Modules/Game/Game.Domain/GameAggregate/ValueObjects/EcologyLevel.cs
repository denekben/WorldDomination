﻿using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.GameAggregate.ValueObjects
{
    public sealed record EcologyLevel
    {
        private const int _minLevel = 0;
        private const int _maxLevel = 100;
        private const int _defaultLevel = 0;

        public int Value { get; private set; }

        private EcologyLevel(int value)
        {
            Value = value;
        }

        public static EcologyLevel Create(int? value = null)
        {
            if(value > _maxLevel || value < _minLevel)
            {
                throw new InvalidArgumentDomainException($"EcologyLevel value {value} is invalid");
            }
            return new EcologyLevel(value ?? _defaultLevel);
        }

        public static implicit operator int(EcologyLevel value) => Create(value);
        public static implicit operator EcologyLevel(int value) => new EcologyLevel(value);
    }
}