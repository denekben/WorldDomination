using Game.Domain.DomainModels.Games.ValueObjects;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.DomainModels.Rooms.ValueObjects;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.Entities
{
    public sealed class Game : DomainEntity
    {
        public IdValueObject RoomId { get; private set; }
        public GameType GameType { get; private set; }
        public bool HasTeams { get; private set; }
        public CurrentRound CurrentRound { get; private set; }
        public EcologyLevel EcologyLevel { get; private set; }

        public List<Country> Countries { get; private set; }
        public Room Room { get; private set; }

        //EF
        private Game() { }

        private Game(GameType gameType, bool hasTeams, Guid roomId)
        {
            RoomId = roomId;
            GameType = gameType;
            HasTeams = hasTeams;
            CurrentRound = CurrentRound.Create();
            EcologyLevel = EcologyLevel.Create();
        }

        public static Game Create(GameType gameType, bool hasTeams, Guid roomId, List<RoomMember> members)
        {
            if (members == null || members.Count == 0)
                throw new BusinessRuleValidationException("Cannot create Game without members");

            if (!CheckMembersDistribution(members))
                throw new BusinessRuleValidationException("Members should be evenly distributed across Countries");

            return new Game(gameType, hasTeams, roomId);
        }

        private bool CheckMembersDistribution(List<RoomMember> members)
        {
            var distribution = members.GroupBy(m => m.CountryId).ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
