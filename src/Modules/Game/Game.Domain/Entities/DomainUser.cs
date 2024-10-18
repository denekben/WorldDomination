using WorldDomination.Shared.Domain;

namespace Game.Domain.Entities
{
    public sealed class DomainUser
    {
        public IdValueObject Id {  get; private set; }
        public string Username { get; private set; }
        public string ProfileImagePath { get; private set; }
    }
}
