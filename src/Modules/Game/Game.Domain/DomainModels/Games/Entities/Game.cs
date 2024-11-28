using Game.Domain.DomainModels.Games.ValueObjects;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.DomainModels.Rooms.ValueObjects;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.Entities
{
    public sealed class Game : DomainEntity
    {
        private const int _defaultEcologyDevelopmentIncreasement = 10;

        public IdValueObject RoomId { get; private set; }
        public GameType GameType { get; private set; }
        public bool HasTeams { get; private set; }
        public bool HasGameStateTimer { get; private set; }
        public RoundQuantity RoundQuantity { get; private set; }
        public GameState GameState { get; private set; }
        public CurrentRound CurrentRound { get; private set; }
        public EcologyLevel EcologyLevel { get; private set; }

        public List<Country> Countries { get; private set; }
        public Room Room { get; private set; }

        //EF
        private Game() { }

        private Game(GameType gameType, bool hasTeams, bool hasGameStateTimer, RoundQuantity roundQuantity, Guid roomId)
        {
            GameType = gameType;
            HasTeams = hasTeams;
            HasGameStateTimer = hasGameStateTimer;
            RoundQuantity = roundQuantity;
            GameState = GameState.Debates;
            CurrentRound = CurrentRound.Create();
            EcologyLevel = EcologyLevel.Create();
            RoomId = roomId;
        }

        public static Game Create(GameType gameType, bool hasTeams, bool hasGameStateTimer, RoundQuantity roundQuantity, Guid roomId, List<RoomMember> members)
        {
            if (members == null || members.Count == 0)
                throw new BusinessRuleValidationException("Cannot create Game without members");

            if (!CheckMembersDistribution(members))
                throw new BusinessRuleValidationException("Members should be evenly distributed across Countries");

            return new Game(gameType, hasTeams, hasGameStateTimer,roundQuantity, roomId);
        }

        private static bool CheckMembersDistribution(List<RoomMember> members)
        {
            var validMembers = members.Where(m => (m.CountryId is not null));

            if (validMembers.Count() != members.Count)
                throw new BusinessRuleValidationException("Members should belong to Country to create a Game");

            var distribution = validMembers.GroupBy(m => m.CountryId.Value).ToDictionary(g => g.Key, g => g.Count());

            int latestQuantity = distribution.First().Value;
            foreach(var memberQuantity in distribution)
            {
                if((memberQuantity.Value - latestQuantity) > 1)
                {
                    return false;
                }
                latestQuantity = memberQuantity.Value;
            }

            return true;
        }

        internal void DevelopEcologyProgram()
        {
            EcologyLevel += _defaultEcologyDevelopmentIncreasement;
        }

        public void ChangeState()
        {

            
            if (GameState == GameState.Debates)
                GameState = GameState.Negotiations;

            else if (GameState == GameState.Negotiations)
                GameState = GameState.OrderMaking;

            else if (GameState == GameState.OrderMaking)
            {
                GameState = GameState.Debates;
                CurrentRound++;
            }
        }
    }
}
