using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Seed
{
    public sealed class GameEventReadModel 
    {
        public Guid Id { get; private set; }
        public string Quality { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
    }
}
