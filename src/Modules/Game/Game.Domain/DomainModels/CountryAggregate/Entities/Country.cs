﻿using Game.Domain.CountryAggregate.ValueObjects;
using Game.Domain.RoomAggregate.Entities;
using DomainGame = Game.Domain.GameAggregate.Entities.Game;
using WorldDomination.Shared.Domain;
using Game.Domain.DomainModels.RoomAggregate.Abstractions;

namespace Game.Domain.CountryAggregate.Entities
{
    public sealed class Country : DomainEntity
    {
        public IdValueObject Id { get; private set; }
        public string CountryName { get; private set; }
        public string FlagImagePath { get; private set; }
        public LivingLevel LivingLevel { get; private set; }
        public Budget Budget { get; private set; }
        public bool HaveNuclearTechnology { get; private set; }
        public NuclearTechnology NuclearTechnology { get; private set; }
        public SanctionCount SanctionCount { get; private set; }

        public List<RoomMember> Players { get; private set; }
        public List<City> Cities { get; private set; }
        public IdValueObject GameId { get; private set; }
        public DomainGame Game { get; private set; }

        //EF
        private Country() { }

        private Country(string countryName, string flagImagePath, Guid gameId)
        {
            Id = Guid.NewGuid();
            CountryName = countryName;
            FlagImagePath = flagImagePath;
            LivingLevel = LivingLevel.Create();
            Budget = Budget.Create();
            HaveNuclearTechnology = false;
            NuclearTechnology = NuclearTechnology.Create();
            SanctionCount = SanctionCount.Create();
            GameId = gameId;
        }

        public static Country Create(string countryName, string flagImagePath,Guid gameId)
        {
            return new Country( countryName, flagImagePath, gameId);
        }
    }
}
