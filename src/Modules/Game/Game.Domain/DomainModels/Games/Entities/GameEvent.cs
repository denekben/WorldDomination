using Game.Domain.DomainModels.Games.ValueObjects;
using WorldDomination.Shared.Domain;

namespace Game.Domain.DomainModels.Games.Entities
{
    public sealed class GameEvent : DomainEntity
    {
        public IdValueObject Id { get; private set; }
        public GameEventQuality Quality { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        private GameEvent() {}

        public GameEvent(GameEventQuality quality, string title, string description)
        {
            Id = Guid.NewGuid();
            Quality = quality;
            Title = title;
            Description = description;
        }
    }
}
