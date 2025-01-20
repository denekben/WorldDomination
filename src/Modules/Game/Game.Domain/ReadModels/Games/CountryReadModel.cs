using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Messaging.Entities;
using Game.Domain.DomainModels.ReadModels.Rooms;
using Game.Domain.ReadModels.Games;
using Game.Domain.ReadModels.Messaging;

namespace Game.Domain.DomainModels.ReadModels.Games
{
    public sealed class CountryReadModel
    {
        public Guid Id { get; private set; }
        public string CountryName { get; private set; }
        public string NormalizedName { get; private set; }
        public string FlagImagePath { get; private set; }
        public int LivingLevel { get; private set; }
        public int Budget { get; private set; }
        public bool HaveNuclearTechnology { get; private set; }
        public int NuclearTecnology { get; private set; }
        public int Income { get; private set; }

        public List<RoomMemberReadModel> Players { get; private set; }
        public List<CityReadModel> Cities { get; private set; }
        public List<SanctionReadModel> OutgoingSanctions { get; private set; }
        public List<SanctionReadModel> IncomingSanctions { get; private set; }
        public List<NegotiationRequestReadModel> OutgoingRequests { get; private set; }
        public List<NegotiationRequestReadModel> IncomingRequests { get; private set; }
        public Guid RoomId { get; private set; }
        public RoomReadModel Room { get; private set; }
        public Guid GameId { get; private set; }
        public GameReadModel Game { get; private set; }
        public OrderReadModel Order { get; private set; }

    }
}
