using DomainGame = Game.Domain.DomainModels.GameAggregate.Entities.Game;
using WorldDomination.Shared.Domain;
using Game.Domain.DomainModels.GameAggregate.ValueObjects;
using Game.Domain.DomainModels.RoomAggregate.Entities;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using Game.Domain.DomainModels.RoomAggregate.ValueObjects;

namespace Game.Domain.DomainModels.GameAggregate.Entities
{
    public sealed class Country : DomainEntity
    {
        public IdValueObject Id { get; set; }
        public string CountryName { get; private set; }
        public string NormalizedName { get; private set; }
        public string FlagImagePath { get; private set; }
        public LivingLevel LivingLevel { get; private set; }
        public Budget Budget { get; private set; }
        public bool HaveNuclearTechnology { get; private set; }
        public NuclearTechnology NuclearTechnology { get; private set; }
        public SanctionCount SanctionCount { get; private set; }

        public List<RoomMember> Players { get; private set; } = [];
        public List<City> Cities { get; private set; } = [];
        public IdValueObject RoomId { get; private set; }
        public Room Room { get; private set; }
        public IdValueObject? GameId { get; private set; }
        public DomainGame Game { get; private set; }

        //EF
        private Country() { }

        private Country(string countryName, string normalizedName, string flagImagePath, Guid roomId)
        {
            Id = Guid.NewGuid();
            CountryName = countryName;
            NormalizedName = normalizedName;
            FlagImagePath = flagImagePath;
            LivingLevel = LivingLevel.Create();
            Budget = Budget.Create();
            HaveNuclearTechnology = false;
            NuclearTechnology = NuclearTechnology.Create();
            SanctionCount = SanctionCount.Create();
            RoomId = roomId;
        }

        public static Country Create(string countryName, string normalizedName, string flagImagePath, Guid roomId)
        {
            return new Country(countryName, normalizedName, flagImagePath, roomId);
        }

        public void AddCity(City city)
        {
            if (Cities.Any(c => (c.Id == city.Id || c.NormalizedName == city.NormalizedName)))
                throw new BusinessRuleValidationException("Cities in Country must be unique");
            Cities.Add(city);
        }

        public void AddPlayer(RoomMember member, bool hasTeams)
        {
            if (Players.Count() == 0)
                member.PromoteToRole(GameRole.President);

            if (Players.Any(p => p.GameRole == GameRole.President) && member.GameRole == GameRole.President)
                member.PromoteToRole(GameRole.Minister);

            if (Players.Any(m => m.GameUserId == member.GameUserId))
                throw new BusinessRuleValidationException("Cannot add same Player in Country");

            if (Players.Count() == 0 && member.GameRole != GameRole.President)
                throw new BusinessRuleValidationException("First Member in Player must be President");

            if (!hasTeams && Players.Count() == 1)
                throw new BusinessRuleValidationException("Cannot add second Player in Country when Room created without teams");

            if (Players.Where(p => (p.GameRole == GameRole.President)).Count() == 1 && (member.GameRole == GameRole.President))
                throw new BusinessRuleValidationException("Country can have only one President");

            Players.Add(member);
        }

        public void RemovePlayer(RoomMember member)
        {
            if (!Players.Any(p => p.GameUserId == member.GameUserId))
                throw new BusinessRuleValidationException("To remove a Player from a Country, they must belong to that Country");

            if (Players.Count() < 1)
                throw new BusinessRuleValidationException("Country must have at least 1 Player to remove");

            Players.Remove(member);
        }

        public RoomMember ElectNewPresident()
        {
            if (Players.Any(p => p.GameRole == GameRole.President))
                throw new BusinessRuleValidationException("Cannot elect a new President in a Country with President");

            var presidentCandidate = Players.FirstOrDefault(p => p.GameRole == GameRole.Minister)
                ?? throw new BusinessRuleValidationException("Country must have at least 1 Minister to promote him to President");

            presidentCandidate.PromoteToRole(GameRole.President);

            return presidentCandidate;
        }
    }
}