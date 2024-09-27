using AppUser.Domain.Entities.Relationships;
using WorldDomination.Shared.Domain;

namespace AppUser.Domain.Entities
{
    public sealed class Achievment
    {
        public IdValueObject Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<UserAchievment> UserAchievments { get; private set; }

        // EF
        private Achievment() { }

        private Achievment(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Achievment CreateNewAchievment(
            string name, string description)
        {
            return new Achievment(name, description);
        }
    }
}
