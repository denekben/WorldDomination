﻿using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.ValueObjects
{
    public sealed record GameRole 
    {
        public static GameRole President => new GameRole("President");
        public static GameRole Minister => new GameRole("Minister");
        public string? Value { get; private set; }

        private GameRole(string? value)
        {
            Value = value;
        }

        public static GameRole Create(string? value = null)
        {
            if(value != "President" && value != "Minister" && value != null)
            {
                throw new InvalidArgumentDomainException($"GameRole value {value} is invalid");
            }

            return new GameRole(value);
        }

        public static implicit operator GameRole(string? value) => Create(value);
        public static implicit operator string?(GameRole role) => role?.Value;
    }
}